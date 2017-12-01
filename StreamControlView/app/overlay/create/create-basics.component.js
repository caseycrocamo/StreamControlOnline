(function () {
    angular.module("main").component("basics", {
        controllerAs: "vm",
        controller: ["overlayResource", "$state", "$timeout", "authService", function (overlayResource, $state, $timeout, authService) {
            var vm = this;
            vm.overlay = {
                Name : '',
                OwnerId : authService.authentication.userName
            };
            vm.name = "";
            vm.message = 'loading...';

            vm.$onInit = function () {
                vm.returnToList = function () {
                    $state.go("overlays");
                };

                vm.save = function () {
                    vm.message = "saving...";
                    vm.overlay.Name = vm.name;
                    
                    $state.go("overlayCreate.element", { overlay: vm.overlay });
                };

            }
        }],
        templateUrl: "app/overlay/create/basics.template.html"
    });
}());