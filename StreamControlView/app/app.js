(function () {
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
                name: "overlays",
                url: "/overlays",
                resolve: {
                    username: function (authService) {
                        authService.fillAuthData();
                        return authService.authentication.userName;
                    }
                },
                template: '<overlay-list username="$resolve.username"></overlay-list>'
            },
            {
                name: "overlayCreate",
                url: "/overlay/create",
                template: '<overlay-create></overlay-create>'
            },
            {
                name: "overlayCreate.basics",
                url: "/basics",
                template: '<create-basics></create-basics>'
            },
            {
                name: "overlayCreate.element",
                url: "/element",
                params: { overlay: null },
                resolve: {
                    overlay: function ($stateParams) {
                        return $stateParams.overlay;
                    }
                },
                template: '<create-element overlay="$resolve.overlay"></create-element>'
            },
            {
                name: "overlayCreate.view",
                url: "/view",
                params: { overlay: null },
                resolve: {
                    overlay: function ($stateParams) {
                        return $stateParams.overlay;
                    }
                },
                template: '<create-view overlay="$resolve.overlay"></create-view>'
            },
            {
                name: "overlay",
                url: "/overlay/{overlayId}",
                resolve: {
                    overlayId: function ($stateParams) {
                        return $stateParams.overlayId;
                    }
                },
                template: '<overlay overlay-id="$resolve.overlayId"></overlay>'
            },
            {
                name: "overlay.edit",
                url: "/edit",
                resolve: {
                    overlayId: function ($stateParams) {
                        return $stateParams.overlayId;
                    }
                },
                template: '<overlay-edit overlay-id="$resolve.overlayId"></overlay-edit>'
            },
            {
                name: "overlay.view",
                url: "/view/{viewId}",
                resolve: {
                    overlayId: function ($stateParams) {
                        return $stateParams.overlayId;
                    },
                    viewId: function ($stateParams) {
                        return $stateParams.viewId;
                    }
                },
                template: '<overlay-view overlay-id="$resolve.overlayId" view-id="$resolve.viewId"></overlay-view>'
            },
            {
                name: "overlay.viewEdit",
                url: "/view/{viewId}/edit",
                resolve: {
                    overlayId: function ($stateParams) {
                        return $stateParams.overlayId;
                    },
                    viewId: function ($stateParams) {
                        return $stateParams.viewId;
                    }
                },
                template: '<view-edit overlay-id="$resolve.overlayId" view-id="$resolve.viewId"></view-edit>'
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
        }
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
        }
    });
}());