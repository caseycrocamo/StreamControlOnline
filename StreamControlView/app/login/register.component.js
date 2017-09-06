(function () {
    angular.module("main").component("register", {
        controllerAs: "vm",
        controller: ['$location', '$timeout', 'authService', function ($location, $timeout, authService) {
            var vm = this;
            vm.savedSuccessfully = false;
            vm.message = "";
            vm.userData = {
                userName: "",
                email: "",
                password: "",
                confirmPassword: ""
            };

            vm.registerUser = function () {
                authService.saveRegistration(vm.userData).then(function (response) {

                    vm.savedSuccessfully = true;
                    vm.message = "User has been registered successfully, you will be redicted to the home page in 2 seconds.";
                    startTimer();

                },
                    function (response) {
                        var errors = [];
                        for (var key in response.data.modelState) {
                            for (var i = 0; i < response.data.modelState[key].length; i++) {
                                errors.push(response.data.modelState[key][i]);
                            }
                        }
                        $scope.message = "Failed to register user due to:" + errors.join(' ');
                    });
            };

            vm.cancel = function () {
                $location.path("");
            };

            var startTimer = function () {
                var timer = $timeout(function () {
                    $timeout.cancel(timer);
                    $location.path('');
                }, 2000);
            }
        }],
        templateUrl: "app/login/register.template.html"
    });
}());