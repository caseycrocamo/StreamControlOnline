(function () {
    angular.module("main").component("viewEdit",
        {
            bindings: {
                scoreboardId: "<",
                viewId: "<"
            },
            controllerAs: "vm",
            controller: ["scoreboardResource", "authService", "$scope", function (scoreboardResource, authService, $scope) {
                var vm = this;
                vm.scoreboard = null;
                vm.message = "loading...";
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
                };
                
            }],
            templateUrl: "app/scoreboard/view/view-edit.template.html"
        });
}());