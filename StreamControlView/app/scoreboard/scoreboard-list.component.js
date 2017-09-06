(function () {
    angular.module("main").component("scoreboardList",
        {
            bindings: {
                scoreboards: "<"
            },
            controllerAs: "vm",
            controller: ["scoreboardResource", "authService", function (scoreboardResource, authService) {
                var vm = this;
                vm.message = "";
                
                vm.$onInit = function () {
                    vm.user = authService.authentication;
                    scoreboardResource.getAll({ username: vm.user.userName })
                        .$promise.then(function (data) {
                            vm.scoreboards = data;
                        });
                }; 
                vm.remove = function (_id) {
                    scoreboardResource.remove({ id: _id })
                        .$promise.then(function () {
                            scoreboardResource.getAll({ username: vm.user.userName })
                                .$promise.then(function (data) {
                                    vm.scoreboards = data;
                                });
                        });
                };
            }],
            templateUrl: "app/scoreboard/scoreboard-list.template.html"
        });
}());