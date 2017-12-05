(function () {
    angular.module("main").component("scoreboardEdit", {
        bindings: {
            scoreboardId: "<"
        },
        controllerAs: "vm",
        controller: ["scoreboardResource", "$location", "$timeout", function (scoreboardResource, $location, $timeout) {
            var vm = this;
            vm.scoreboard = {};
            vm.message = 'loading...';

            vm.$onInit = function () {
                scoreboardResource.get({ id: vm.scoreboardId })
                    .$promise
                    .then(
                    //on success
                    function (data) {
                        vm.message = "";
                        vm.scoreboard = data;
                        vm.originalScoreboard = angular.copy(data);
                    },
                    //on failure
                    function () {
                        vm.message = "this shit didnt load, are you logged in?";
                    });

                vm.cancel = function () {
                    vm.scoreboard = vm.originalScoreboard;
                    vm.message = "Changes Reverted";
                };

                vm.returnToList = function () {
                    $location.path("/scoreboards");
                };

                vm.save = function () {
                    vm.message = "saving...";
                    scoreboardResource.update({ id: vm.scoreboardId }, vm.scoreboard).$promise.then(function () {
                        vm.message = "wow you saved!";
                        scoreboardResource.get({ id: vm.scoreboardId }).$promise.then(function (data) {
                            vm.scoreboard = data;
                            vm.originalScoreboard = angular.copy(data);
                        });
                    });
                };
            };
            

            
        }],
        templateUrl: "app/scoreboard/scoreboard-edit.template.html"
    });
}());