(function () {
    angular.module("main").component("overlay",
        {
            bindings: {
                OverlayID: "<"
            },
            controllerAs: "vm",
            controller: [function () {
                var vm = this;
            }],
            templateUrl: "app/overlay/overlay.template.html"
        });
}());