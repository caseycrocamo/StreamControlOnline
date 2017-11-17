(function () {
    angular.module("main").component("viewEdit",
        {
            bindings: {
                scoreboardId: "<",
                viewId: "<"
            },
            controllerAs: "vm",
            controller: ["scoreboardResource", "authService", "$scope", "$filter", function (scoreboardResource, authService, $scope, $filter) {
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

                

                function currentIdChanged() {
                    if (vm.view !== undefined) {
                        var filtered = $filter('getById')(vm.view.Style, vm.currentId);
                        console.log(filtered);
                        vm.currentStyle = filtered;
                    };
                };
                
            }],
            templateUrl: "app/scoreboard/view/view-edit.template.html"
        });
}());