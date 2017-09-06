(function () {
    angular.module("main").component("scoreboardCreate",
        {
            controllerAs: "vm",
            controller: ["scoreboardResource", "$state", "$timeout", "authService", function (scoreboardResource, $state, $timeout, authService) {
                var vm = this;
            }],
            templateUrl: "app/scoreboard/scoreboard-create.template.html"
        });
}());