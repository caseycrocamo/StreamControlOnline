﻿(function () {
    "use strict";
    var app = angular.module("main", ["LocalStorageModule","common", "ui.router"]);

    app.value('componentBorders', false);

    app.run(function (componentBorders) {
        if (componentBorders) {
            if (app._invokeQueue) {
                app._invokeQueue.forEach(function (item) {
                    if (item[1] === 'component') {
                        var componentName = item[2][0];
                        var componentProperties = item[2][1];
                        if (componentProperties.templateUrl) {
                            var templateUrl = componentProperties.templateUrl;
                            delete componentProperties.templateUrl;
                            componentProperties.template = '<div class="component-borders"><b>' + componentName + '</b><div ng-include="\'' + templateUrl + '\'"></div></div>';
                        }
                        else {
                            var template = '<div class="component-borders">' + componentName + '<div>' + componentProperties.template + '</div></div>';
                            componentProperties.template = template;
                        }
                    }
                });
            }
        }
    });

    app.config(function (localStorageServiceProvider) {
        localStorageServiceProvider
            .setPrefix('StreamControl');
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('sessionInjector');
    });

    app.config(function ($stateProvider) {
        var states = [
            {
                name: "home",
                url: "",
                template: '<home></home>'
            },
            {
                name: "home2",
                url: "/",
                template: '<home></home>'
            },
            {
                name: "login",
                url: "/login",
                template: '<login></login>'
            },
            {
                name: "register",
                url: "/register",
                template: '<register></register>'
            },
            {
                name: "scoreboards",
                url: "/scoreboards",
                resolve: {
                    username: function (authService) {
                        authService.fillAuthData();
                        return authService.authentication.userName;
                    }
                },
                template: '<scoreboard-list username="$resolve.username"></scoreboard-list>'
            },
            {
                name: "scoreboardCreate",
                url: "/scoreboard/create",
                template: '<scoreboard-create></scoreboard-create>'
            },
            {
                name: "scoreboardCreate.basics",
                url: "/basics",
                template: '<basics></basics>'
            },
            {
                name: "scoreboardCreate.element",
                url: "/element",
                params: { scoreboard: null },
                resolve: {
                    scoreboard: function ($stateParams) {
                        return $stateParams.scoreboard;
                    }
                },
                template: '<element scoreboard="$resolve.scoreboard"></element>'
            },
            {
                name: "scoreboardCreate.view",
                url: "/view",
                params: { scoreboard: null },
                resolve: {
                    scoreboard: function ($stateParams) {
                        return $stateParams.scoreboard;
                    }
                },
                template: '<view scoreboard="$resolve.scoreboard"></view>'
            },
            {
                name: "scoreboard",
                url: "/scoreboard/{scoreboardId}",
                resolve: {
                    scoreboardId: function ($stateParams) {
                        return $stateParams.scoreboardId;
                    }
                },
                template: '<scoreboard scoreboard-id="$resolve.scoreboardId"></scoreboard>'
            },
            {
                name: "scoreboard.edit",
                url: "/edit",
                resolve: {
                    scoreboardId: function ($stateParams) {
                        return $stateParams.scoreboardId;
                    }
                },
                template: '<scoreboard-edit scoreboard-id="$resolve.scoreboardId"></scoreboard-edit>'
            },
            {
                name: "scoreboard.view",
                url: "/view/{viewId}",
                resolve: {
                    scoreboardId: function ($stateParams) {
                        return $stateParams.scoreboardId;
                    },
                    viewId: function ($stateParams) {
                        return $stateParams.viewId;
                    }
                },
                template: '<scoreboard-view scoreboard-id="$resolve.scoreboardId" view-id="$resolve.viewId"></scoreboard-view>'
            },
            {
                name: "scoreboard.viewEdit",
                url: "/view/{viewId}/edit",
                resolve: {
                    scoreboardId: function ($stateParams) {
                        return $stateParams.scoreboardId;
                    },
                    viewId: function ($stateParams) {
                        return $stateParams.viewId;
                    }
                },
                template: '<view-edit scoreboard-id="$resolve.scoreboardId" view-id="$resolve.viewId"></view-edit>'
            }
        ];
        states.forEach(function (state) {
            $stateProvider.state(state);
        });
    });

    app.filter('getById', function () {
        return function (input, id) {
            let i = 0, len = input.length;
            for (; i < len; i++) {
                if (input[i].DivID === id) {
                    return input[i];
                }
            }
            return null;
        };
    });

    app.directive('onFinishRender', function ($timeout) {
        return {
            restrict: 'A',
            link: function (scope, element, attr) {
                if (scope.$last === true) {
                    $timeout(function () {
                        scope.$emit(attr.onFinishRender);
                    });
                }
            }
        };
    });
}());