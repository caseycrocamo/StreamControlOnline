(function () {
    angular.module("main").component("scoreboardList",
        {
            bindings: {
                username: "<"
            },
            controllerAs: "vm",
            controller: ["scoreboardResource", function (scoreboardResource) {
                var vm = this;
                vm.message = "";
                
                vm.$onInit = function () {
                    vm.scoreboards = {};
                    scoreboardResource.getAll({ username: vm.username })
                        .$promise.then(function (data) {
                            vm.scoreboards = data;
                        });
                }; 
                vm.remove = function (_id) {
                    scoreboardResource.remove({ id: _id })
                        .$promise.then(function () {
                            scoreboardResource.getAll({ username: vm.username })
                                .$promise.then(function (data) {
                                    vm.scoreboards = data;
                                });
                        });
                };
            }],
            templateUrl: "app/scoreboard/scoreboard-list.template.html"
        });
}());