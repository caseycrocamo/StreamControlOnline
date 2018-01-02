(function () {
    angular.module("main").component("viewEdit",
        {
            bindings: {
                scoreboardId: "<",
                viewId: "<"
            },
            controllerAs: "vm",
            controller: ["scoreboardResource", "authService", "$scope", "$filter", "$location", function (scoreboardResource, authService, $scope, $filter, $location) {
                var vm = this;
                vm.scoreboard = null;
                vm.message = "loading...";
                vm.currentId = "";
                vm.currentStyle = {};
                vm.$onInit = function () {
                    scoreboardResource.get({ id: vm.scoreboardId })
                        .$promise
                        .then(function (data) {
                            vm.message = "";
                            vm.scoreboard = data;
                            for (let i = 0; i < vm.scoreboard.Views.length; i++) {
                                if (vm.scoreboard.Views[i].ViewID == vm.viewId) {
                                    vm.view = vm.scoreboard.Views[i];
                                }
                            };
                        })

                    $scope.$watch('vm.currentId', currentIdChanged);
                };

                vm.currentSelection = function (event) {
                    var id = event.srcElement.id;
                    console.log(id);
                    vm.currentId = id;
                };

                vm.cancel = function () {
                    vm.currentStyle = vm.originalStyle;
                    vm.message = "Changes Reverted";
                };

                vm.returnToList = function () {
                    $location.path("/scoreboards");
                };

                vm.save = function () {
                    vm.message = "saving...";
                    scoreboardResource.updateStyle({ id: vm.currentStyle.StyleID }, vm.currentStyle).$promise.then(function () {
                        vm.message = "wow you saved!";
                        scoreboardResource.get({ id: vm.scoreboardId }).$promise.then(function (data) {
                            vm.scoreboard = data;
                            vm.originalScoreboard = angular.copy(data);
                        });
                    });
                };

                function currentIdChanged() {
                    if (vm.view !== undefined) {
                        let styleObj = $filter('getById')(vm.view.Style, vm.currentId);
                        vm.originalStyle = styleObj;
                        vm.currentStyle = styleObj;
                    };
                };

                
            }],
            templateUrl: "app/scoreboard/view/view-edit.template.html"
        });
}());