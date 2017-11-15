(function () {
    angular.module("main").component("view", {
        bindings: {
            scoreboard: "<"
        },
        controllerAs: "vm",
        controller: ["scoreboardResource", "$state", "$timeout", function (scoreboardResource, $state, $timeout) {
            var vm = this;
            vm.$onInit = function () {
                console.log(vm.scoreboard);
                vm.views = [];
            }

            vm.addView = function () {
                vm.views.push({
                    Name: ""
                });
            }

            vm.returnToList = function () {
                $state.go("scoreboards");
            };

            vm.save = function () {
                vm.scoreboard.Views = vm.views;
                scoreboardResource.create(vm.scoreboard)
                        .$promise
                        .then(
                            //on success
                        function (data) {
                            $state.go("scoreboards");
                        },
                            //on failure
                        function () {
                        vm.message = "creation failed";
                    });
            };
        }],
        templateUrl: "app/scoreboard/create/view.template.html"
    });
}());