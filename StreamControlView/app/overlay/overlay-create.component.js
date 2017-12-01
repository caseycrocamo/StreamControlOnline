(function () {
    angular.module("main").component("overlayCreate",
        {
            controllerAs: "vm",
            controller: ["overlayResource", "$state", "$timeout", "authService", function (overlayResource, $state, $timeout, authService) {
                var vm = this;
            }],
            templateUrl: "app/overlay/overlay-create.template.html"
        });
}());