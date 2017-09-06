(function () {
    angular.module("main").component("basics", {
        controllerAs: "vm",
        controller: ["scoreboardResource", "$state", "$timeout", "authService", function (scoreboardResource, $state, $timeout, authService) {
            var vm = this;
            vm.scoreboard = {
                Name : '',
                OwnerId : authService.authentication.userName
            };
            vm.name = "";
            vm.message = 'loading...';

            vm.$onInit = function () {
                vm.returnToList = function () {
                    $state.go("scoreboards");
                };

                vm.save = function () {
                    vm.message = "saving...";
                    vm.scoreboard.Name = vm.name;
                    scoreboardResource.create(vm.scoreboard)
                        .$promise
                        .then(
                            //on success
                        function (data) {
                            $state.go("^.element", { scoreboardId: data.ScoreboardID });
                        },
                            //on failure
                        function () {
                        vm.message = "creation failed";
                    });
                };

            }
        }],
        templateUrl: "app/scoreboard/create/basics.template.html"
    });
}());