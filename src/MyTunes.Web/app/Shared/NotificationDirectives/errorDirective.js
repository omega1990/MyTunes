
application.directive('errorMessage',
    ['$timeout',
        function ($timeout) {
            return {
                restrict: 'A',
                link: function (scope, elem, attrs) {
                    scope.$watch('error', function (newValue, oldValue) {
                        if (newValue != undefined) {
                            var notification = noty({ text: newValue, layout: 'bottomRight', type: 'error',  });

                            $timeout(function () {
                                notification.close();
                            }, 2000);
                        }
                    });
                }
            };
        }
    ]
);