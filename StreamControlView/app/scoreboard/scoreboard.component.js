(function () {
    angular.module("main").component("scoreboard",
        {
            bindings: {
                scoreboardId: "<"
            },
            controllerAs: "vm",
            controller: [function () {
                var vm = this;
            }],
            templateUrl: "app/scoreboard/scoreboard.template.html"
        });
}());