
application.directive('successMessage',
    ['$timeout',
        function ($timeout) {
            return {
                restrict: 'A',
                link: function (scope, elem, attrs) {
                    scope.$watch('success', function (newValue, oldValue) {
                        if (newValue != undefined) {
                            var notification = noty({ text: newValue, layout: 'bottomRight', type: 'success' });

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