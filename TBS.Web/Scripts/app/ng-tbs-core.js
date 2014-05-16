window.app = angular.module('TDev', ['ui.bootstrap', 'ngRoute', 'ngResource', 'ngIdle']);
window.app.config(function ($idleProvider, $keepaliveProvider) {
    $idleProvider.idleDuration(1000);
    $idleProvider.warningDuration(1000);
    $keepaliveProvider.interval(1000);
})
.run(function ($rootScope, $idle, $log, $keepalive) {
    $idle.watch();

    $log.debug('app started.');
});

