(function () {
    angular.module("main").component("element", {
        bindings: {
            scoreboard: "<"
        },
        controllerAs: "vm",
        controller: ["scoreboardResource", "$state", "$timeout", function (scoreboardResource, $state, $timeout) {
            var vm = this;
            vm.$onInit = function () {
                vm.fields = [];
                vm.players = [];
            }

            vm.addField = function () {
                vm.fields.push({
                    Label: ""
                });
            }

            vm.addPlayer = function () {
                vm.players.push({
                    Label: ""
                });
            }

            vm.returnToList = function () {
                $state.go("scoreboards");
            };

            vm.save = function () {
                vm.scoreboard.Players = vm.players;
                vm.scoreboard.Fields = vm.fields;
                //scoreboardResource.create(vm.scoreboard)
                //    .$promise
                //    .then(
                //        //on success
                //    function (data) {
                //        $state.go("^.element", { scoreboardId: data.ScoreboardID });
                //    },
                //        //on failure
                //    function () {
                //    vm.message = "creation failed";
                //});
                $state.go("scoreboardCreate.view", { scoreboard: vm.scoreboard });
            };
        }],
        templateUrl: "app/scoreboard/create/element.template.html"
    });
}());