(function () {
    angular.module("main").component("scoreboardProperties", {
        controllerAs: "vm",
        controller: ["scoreboardResource", function (scoreboardResource) {
            var vm = this;
            vm.scoreboard = {};
            vm.message = 'loading...';
            vm.id = 1;

            scoreboardResource.get({ id: vm.id })
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
                    vm.message = "yo shit didnt load"
                });

            vm.cancel = function () {
                vm.scoreboard = vm.originalScoreboard;
                vm.message = "Changes Reverted";
            };

            vm.returnToList = function () {
                $location.path("");
            };

            vm.save = function () {
                vm.message = "saving...";
                scoreboardResource.update({ id: vm.id }, vm.scoreboard).$promise.then(function () {
                    vm.message = "wow you saved! If you would like to learn how obama turned the frogs gay - hit cancel";
                    scoreboardResource.get({ id: vm.id }).$promise.then(function (data) {
                        vm.scoreboard = data;
                        vm.originalScoreboard = angular.copy(data);
                    });
                });
            };
        }],
        templateUrl: "app/scoreboard/scoreboard-properties.template.html"
    });
}());