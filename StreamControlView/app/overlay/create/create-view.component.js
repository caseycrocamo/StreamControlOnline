(function () {
    angular.module("main").component("view", {
        bindings: {
            overlay: "<"
        },
        controllerAs: "vm",
        controller: ["overlayResource", "$state", "$timeout", function (overlayResource, $state, $timeout) {
            var vm = this;
            vm.$onInit = function () {
                console.log(vm.overlay);
                vm.views = [];
            }

            vm.addView = function () {
                vm.views.push({
                    Name: ""
                });
            }

            vm.returnToList = function () {
                $state.go("overlays");
            };

            vm.save = function () {
                vm.overlay.Views = vm.views;
                overlayResource.create(vm.overlay)
                        .$promise
                        .then(
                            //on success
                        function (data) {
                            $state.go("overlays");
                        },
                            //on failure
                        function () {
                        vm.message = "creation failed";
                    });
            };
        }],
        templateUrl: "app/overlay/create/view.template.html"
    });
}());