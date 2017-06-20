/**
 * @license AngularJS v1.6.3
 * (c) 2010-2017 Google, Inc. http://angularjs.org
 * License: MIT
 */
(function(window, angular) {'use strict';

/* global shallowCopy: true */

/**
 * Creates a shallow copy of an object, an array or a primitive.
 *
 * Assumes that there are no proto properties for objects.
 */
function shallowCopy(src, dst) {
  if (isArray(src)) {
    dst = dst || [];

    for (var i = 0, ii = src.length; i < ii; i++) {
      dst[i] = src[i];
    }
  } else if (isObject(src)) {
    dst = dst || {};

    for (var key in src) {
      if (!(key.charAt(0) === '$' && key.charAt(1) === '$')) {
        dst[key] = src[key];
      }
    }
  }

  return dst || src;
}

/* global shallowCopy: false */

// `isArray` and `isObject` are necessary for `shallowCopy()` (included via `src/shallowCopy.js`).
// They are initialized inside the `$RouteProvider`, to ensure `window.angular` is available.
var isArray;
var isObject;
var isDefined;
var noop;

/**
 * @ngdoc module
 * @name ngRoute
 * @description
 *
 * # ngRoute
 *
 * The `ngRoute` module provides routing and deeplinking services and directives for angular apps.
 *
 * ## Example
 * See {@link ngRoute.$route#example $route} for an example of configuring and using `ngRoute`.
 *
 *
 * <div doc-module-components="ngRoute"></div>
 */
/* global -ngRouteModule */
var ngRouteModule = angular.
  module('ngRoute', []).
  info({ angularVersion: '1.6.3' }).
  provider('$route', $RouteProvider).
  // Ensure `$route` will be instantiated in time to capture the initial `$locationChangeSuccess`
  // event (unless explicitly disabled). This is necessary in case `ngView` is included in an
  // asynchronously loaded template.
  run(instantiateRoute);
var $routeMinErr = angular.$$minErr('ngRoute');
var isEagerInstantiationEnabled;


/**
 * @ngdoc provider
 * @name $routeProvider
 * @this
 *
 * @description
 *
 * Used for configuring routes.
 *
 * ## Example
 * See {@link ngRoute.$route#example $route} for an example of configuring and using `ngRoute`.
 *
 * ## Dependencies
 * Requires the {@link ngRoute `ngRoute`} module to be installed.
 */
function $RouteProvider() {
  isArray = angular.isArray;
  isObject = angular.isObject;
  isDefined = angular.isDefined;
  noop = angular.noop;

  function inherit(parent, extra) {
    return angular.extend(Object.create(parent), extra);
  }

  var routes = {};

  /**
   * @ngdoc method
   * @name $routeProvider#when
   *
   * @param {string} path Route path (matched against `$location.path`). If `$location.path`
   *    contains redundant trailing slash or is missing one, the route will still match and the
   *    `$location.path` will be updated to add or drop the trailing slash to exactly match the
   *    route definition.
   *
   *    * `path` can contain named groups starting with a colon: e.g. `:name`. All characters up
   *        to the next slash are matched and stored in `$routeParams` under the given `name`
   *        when the route matches.
   *    * `path` can contain named groups starting with a colon and ending with a star:
   *        e.g.`:name*`. All characters are eagerly stored in `$routeParams` under the given `name`
   *        when the route matches.
   *    * `path` can contain optional named groups with a question mark: e.g.`:name?`.
   *
   *    For example, routes like `/color/:color/largecode/:largecode*\/edit` will match
   *    `/color/brown/largecode/code/with/slashes/edit` and extract:
   *
   *    * `color: brown`
   *    * `largecode: code/with/slashes`.
   *
   *
   * @param {Object} route Mapping information to be assigned to `$route.current` on route
   *    match.
   *
   *    Object properties:
   *
   *    - `controller` â€“ `{(string|Function)=}` â€“ Controller fn that should be associated with
   *      newly created scope or the name of a {@link angular.Module#controller registered
   *      controller} if passed as a string.
   *    - `controllerAs` â€“ `{string=}` â€“ An identifier name for a reference to the controller.
   *      If present, the controller will be published to scope under the `controllerAs` name.
   *    - `template` â€“ `{(string|Function)=}` â€“ html template as a string or a function that
   *      returns an html template as a string which should be used by {@link
   *      ngRoute.directive:ngView ngView} or {@link ng.directive:ngInclude ngInclude} directives.
   *      This property takes precedence over `templateUrl`.
   *
   *      If `template` is a function, it will be called with the following parameters:
   *
   *      - `{Array.<Object>}` - route parameters extracted from the current
   *        `$location.path()` by applying the current route
   *
   *      One of `template` or `templateUrl` is required.
   *
   *    - `templateUrl` â€“ `{(string|Function)=}` â€“ path or function that returns a path to an html
   *      template that should be used by {@link ngRoute.directive:ngView ngView}.
   *
   *      If `templateUrl` is a function, it will be called with the following parameters:
   *
   *      - `{Array.<Object>}` - route parameters extracted from the current
   *        `$location.path()` by applying the current route
   *
   *      One of `templateUrl` or `template` is required.
   *
   *    - `resolve` - `{Object.<string, Function>=}` - An optional map of dependencies which should
   *      be injected into the controller. If any of these dependencies are promises, the router
   *      will wait for them all to be resolved or one to be rejected before the controller is
   *      instantiated.
   *      If all the promises are resolved successfully, the values of the resolved promises are
   *      injected and {@link ngRoute.$route#$routeChangeSuccess $routeChangeSuccess} event is
   *      fired. If any of the promises are rejected the
   *      {@link ngRoute.$route#$routeChangeError $routeChangeError} event is fired.
   *      For easier access to the resolved dependencies from the template, the `resolve` map will
   *      be available on the scope of the route, under `$resolve` (by default) or a custom name
   *      specified by the `resolveAs` property (see below). This can be particularly useful, when
   *      working with {@link angular.Module#component components} as route templates.<br />
   *      <div class="alert alert-warning">
   *        **Note:** If your scope already contains a property with this name, it will be hidden
   *        or overwritten. Make sure, you specify an appropriate name for this property, that
   *        does not collide with other properties on the scope.
   *      </div>
   *      The map object is:
   *
   *      - `key` â€“ `{string}`: a name of a dependency to be injected into the controller.
   *      - `factory` - `{string|Function}`: If `string` then it is an alias for a service.
   *        Otherwise if function, then it is {@link auto.$injector#invoke injected}
   *        and the return value is treated as the dependency. If the result is a promise, it is
   *        resolved before its value is injected into the controller. Be aware that
   *        `ngRoute.$routeParams` will still refer to the previous route within these resolve
   *        functions.  Use `$route.current.params` to access the new route parameters, instead.
   *
   *    - `resolveAs` - `{string=}` - The name under which the `resolve` map will be available on
   *      the scope of the route. If omitted, defaults to `$resolve`.
   *
   *    - `redirectTo` â€“ `{(string|Function)=}` â€“ value to update
   *      {@link ng.$location $location} path with and trigger route redirection.
   *
   *      If `redirectTo` is a function, it will be called with the following parameters:
   *
   *      - `{Object.<string>}` - route parameters extracted from the current
   *        `$location.path()` by applying the current route templateUrl.
   *      - `{string}` - current `$location.path()`
   *      - `{Object}` - current `$location.search()`
   *
   *      The custom `redirectTo` function is expected to return a string which will be used
   *      to update `$location.url()`. If the function throws an error, no further processing will
   *      take place and the {@link ngRoute.$route#$routeChangeError $routeChangeError} event will
   *      be fired.
   *
   *      Routes that specify `redirectTo` will not have their controllers, template functions
   *      or resolves called, the `$location` will be changed to the redirect url and route
   *      processing will stop. The exception to this is if the `redirectTo` is a function that
   *      returns `undefined`. In this case the route transition occurs as though there was no
   *      redirection.
   *
   *    - `resolveRedirectTo` â€“ `{Function=}` â€“ a function that will (eventually) return the value
   *      to update {@link ng.$location $location} URL with and trigger route redirection. In
   *      contrast to `redirectTo`, dependencies can be injected into `resolveRedirectTo` and the
   *      return value can be either a string or a promise that will be resolved to a string.
   *
   *      Similar to `redirectTo`, if the return value is `undefined` (or a promise that gets
   *      resolved to `undefined`), no redirection takes place and the route transition occurs as
   *      though there was no redirection.
   *
   *      If the function throws an error or the returned promise gets rejected, no further
   *      processing will take place and the
   *      {@link ngRoute.$route#$routeChangeError $routeChangeError} event will be fired.
   *
   *      `redirectTo` takes precedence over `resolveRedirectTo`, so specifying both on the same
   *      route definition, will cause the latter to be ignored.
   *
   *    - `[reloadOnSearch=true]` - `{boolean=}` - reload route when only `$location.search()`
   *      or `$location.hash()` changes.
   *
   *      If the option is set to `false` and url in the browser changes, then
   *      `$routeUpdate` event is broadcasted on the root scope.
   *
   *    - `[caseInsensitiveMatch=false]` - `{boolean=}` - match routes without being case sensitive
   *
   *      If the option is set to `true`, then the particular route can be matched without being
   *      case sensitive
   *
   * @returns {Object} self
   *
   * @description
   * Adds a new route definition to the `$route` service.
   */
  this.when = function(path, route) {
    //copy original route object to preserve params inherited from proto chain
    var routeCopy = shallowCopy(route);
    if (angular.isUndefined(routeCopy.reloadOnSearch)) {
      routeCopy.reloadOnSearch = true;
    }
    if (angular.isUndefined(routeCopy.caseInsensitiveMatch)) {
      routeCopy.caseInsensitiveMatch = this.caseInsensitiveMatch;
    }
    routes[path] = angular.extend(
      routeCopy,
      path && pathRegExp(path, routeCopy)
    );

    // create redirection for trailing slashes
    if (path) {
      var redirectPath = (path[path.length - 1] === '/')
            ? path.substr(0, path.length - 1)
            : path + '/';

      routes[redirectPath] = angular.extend(
        {redirectTo: path},
        pathRegExp(redirectPath, routeCopy)
      );
    }

    return this;
  };

  /**
   * @ngdoc property
   * @name $routeProvider#caseInsensitiveMatch
   * @description
   *
   * A boolean property indicating if routes defined
   * using this provider should be matched using a case insensitive
   * algorithm. Defaults to `false`.
   */
  this.caseInsensitiveMatch = false;

   /**
    * @param path {string} path
    * @param opts {Object} options
    * @return {?Object}
    *
    * @description
    * Normalizes the given path, returning a regular expression
    * and the original path.
    *
    * Inspired by pathRexp in visionmedia/express/lib/utils.js.
    */
  function pathRegExp(path, opts) {
    var insensitive = opts.caseInsensitiveMatch,
        ret = {
          originalPath: path,
          regexp: path
        },
        keys = ret.keys = [];

    path = path
      .replace(/([().])/g, '\\$1')
      .replace(/(\/)?:(\w+)(\*\?|[?*])?/g, function(_, slash, key, option) {
        var optional = (option === '?' || option === '*?') ? '?' : null;
        var star = (option === '*' || option === '*?') ? '*' : null;
        keys.push({ name: key, optional: !!optional });
        slash = slash || '';
        return ''
          + (optional ? '' : slash)
          + '(?:'
          + (optional ? slash : '')
          + (star && '(.+?)' || '([^/]+)')
          + (optional || '')
          + ')'
          + (optional || '');
      })
      .replace(/([/$*])/g, '\\$1');

    ret.regexp = new RegExp('^' + path + '$', insensitive ? 'i' : '');
    return ret;
  }

  /**
   * @ngdoc method
   * @name $routeProvider#otherwise
   *
   * @description
   * Sets route definition that will be used on route change when no other route definition
   * is matched.
   *
   * @param {Object|string} params Mapping information to be assigned to `$route.current`.
   * If called with a string, the value maps to `redirectTo`.
   * @returns {Object} self
   */
  this.otherwise = function(params) {
    if (typeof params === 'string') {
      params = {redirectTo: params};
    }
    this.when(null, params);
    return this;
  };

  /**
   * @ngdoc method
   * @name $routeProvider#eagerInstantiationEnabled
   * @kind function
   *
   * @description
   * Call this method as a setter to enable/disable eager instantiation of the
   * {@link ngRoute.$route $route} service upon application bootstrap. You can also call it as a
   * getter (i.e. without any arguments) to get the current value of the
   * `eagerInstantiationEnabled` flag.
   *
   * Instantiating `$route` early is necessary for capturing the initial
   * {@link ng.$location#$locationChangeStart $locationChangeStart} event and navigating to the
   * appropriate route. Usually, `$route` is instantiated in time by the
   * {@link ngRoute.ngView ngView} directive. Yet, in cases where `ngView` is included in an
   * asynchronously loaded template (e.g. in another directive's template), the directive factory
   * might not be called soon enough for `$route` to be instantiated _before_ the initial
   * `$locationChangeSuccess` event is fired. Eager instantiation ensures that `$route` is always
   * instantiated in time, regardless of when `ngView` will be loaded.
   *
   * The default value is true.
   *
   * **Note**:<br />
   * You may want to disable the default behavior when unit-testing modules that depend on
   * `ngRoute`, in order to avoid an unexpected request for the default route's template.
   *
   * @param {boolean=} enabled - If provided, update the internal `eagerInstantiationEnabled` flag.
   *
   * @returns {*} The current value of the `eagerInstantiationEnabled` flag if used as a getter or
   *     itself (for chaining) if used as a setter.
   */
  isEagerInstantiationEnabled = true;
  this.eagerInstantiationEnabled = function eagerInstantiationEnabled(enabled) {
    if (isDefined(enabled)) {
      isEagerInstantiationEnabled = enabled;
      return this;
    }

    return isEagerInstantiationEnabled;
  };


  this.$get = ['$rootScope',
               '$location',
               '$routeParams',
               '$q',
               '$injector',
               '$templateRequest',
               '$sce',
               '$browser',
      function($rootScope, $location, $routeParams, $q, $injector, $templateRequest, $sce, $browser) {

    /**
     * @ngdoc service
     * @name $route
     * @requires $location
     * @requires $routeParams
     *
     * @property {Object} current Reference to the current route definition.
     * The route definition contains:
     *
     *   - `controller`: The controller constructor as defined in the route definition.
     *   - `locals`: A map of locals which is used by {@link ng.$controller $controller} service for
     *     controller instantiation. The `locals` contain
     *     the resolved values of the `resolve` map. Additionally the `locals` also contain:
     *
     *     - `$scope` - The current route scope.
     *     - `$template` - The current route template HTML.
     *
     *     The `locals` will be assigned to the route scope's `$resolve` property. You can override
     *     the property name, using `resolveAs` in the route definition. See
     *     {@link ngRoute.$routeProvider $routeProvider} for more info.
     *
     * @property {Object} routes Object with all route configuration Objects as its properties.
     *
     * @description
     * `$route` is used for deep-linking URLs to controllers and views (HTML partials).
     * It watches `$location.url()` and tries to map the path to an existing route definition.
     *
     * Requires the {@link ngRoute `ngRoute`} module to be installed.
     *
     * You can define routes through {@link ngRoute.$routeProvider $routeProvider}'s API.
     *
     * The `$route` service is typically used in conjunction with the
     * {@link ngRoute.directive:ngView `ngView`} directive and the
     * {@link ngRoute.$routeParams `$routeParams`} service.
     *
     * @example
     * This example shows how changing the URL hash causes the `$route` to match a route against the
     * URL, and the `ngView` pulls in the partial.
     *
     * <example name="$route-service" module="ngRouteExample"
     *          deps="angular-route.js" fixBase="true">
     *   <file name="index.html">
     *     <div ng-controller="MainController">
     *       Choose:
     *       <a href="Book/Moby">Moby</a> |
     *       <a href="Book/Moby/ch/1">Moby: Ch1</a> |
     *       <a href="Book/Gatsby">Gatsby</a> |
     *       <a href="Book/Gatsby/ch/4?key=value">Gatsby: Ch4</a> |
     *       <a href="Book/Scarlet">Scarlet Letter</a><br/>
     *
     *       <div ng-view></div>
     *
     *       <hr />
     *
     *       <pre>$location.path() = {{$location.path()}}</pre>
     *       <pre>$route.current.templateUrl = {{$route.current.templateUrl}}</pre>
     *       <pre>$route.current.params = {{$route.current.params}}</pre>
     *       <pre>$route.current.scope.name = {{$route.current.scope.name}}</pre>
     *       <pre>$routeParams = {{$routeParams}}</pre>
     *     </div>
     *   </file>
     *
     *   <file name="book.html">
     *     controller: {{name}}<br />
     *     Book Id: {{params.bookId}}<br />
     *   </file>
     *
     *   <file name="chapter.html">
     *     controller: {{name}}<br />
     *     Book Id: {{params.bookId}}<br />
     *     Chapter Id: {{params.chapterId}}
     *   </file>
     *
     *   <file name="script.js">
     *     angular.module('ngRouteExample', ['ngRoute'])
     *
     *      .controller('MainController', function($scope, $route, $routeParams, $location) {
     *          $scope.$route = $route;
     *          $scope.$location = $location;
     *          $scope.$routeParams = $routeParams;
     *      })
     *
     *      .controller('BookController', function($scope, $routeParams) {
     *          $scope.name = 'BookController';
     *          $scope.params = $routeParams;
     *      })
     *
     *      .controller('ChapterController', function($scope, $routeParams) {
     *          $scope.name = 'ChapterController';
     *          $scope.params = $routeParams;
     *      })
     *
     *     .config(function($routeProvider, $locationProvider) {
     *       $routeProvider
     *        .when('/Book/:bookId', {
     *         templateUrl: 'book.html',
     *         controller: 'BookController',
     *         resolve: {
     *           // I will cause a 1 second delay
     *           delay: function($q, $timeout) {
     *             var delay = $q.defer();
     *             $timeout(delay.resolve, 1000);
     *             return delay.promise;
     *           }
     *         }
     *       })
     *       .when('/Book/:bookId/ch/:chapterId', {
     *         templateUrl: 'chapter.html',
     *         controller: 'ChapterController'
     *       });
     *
     *       // configure html5 to get links working on jsfiddle
     *       $locationProvider.html5Mode(true);
     *     });
     *
     *   </file>
     *
     *   <file name="protractor.js" type="protractor">
     *     it('should load and compile correct template', function() {
     *       element(by.linkText('Moby: Ch1')).click();
     *       var content = element(by.css('[ng-view]')).getText();
     *       expect(content).toMatch(/controller: ChapterController/);
     *       expect(content).toMatch(/Book Id: Moby/);
     *       expect(content).toMatch(/Chapter Id: 1/);
     *
     *       element(by.partialLinkText('Scarlet')).click();
     *
     *       content = element(by.css('[ng-view]')).getText();
     *       expect(content).toMatch(/controller: BookController/);
     *       expect(content).toMatch(/Book Id: Scarlet/);
     *     });
     *   </file>
     * </example>
     */

    /**
     * @ngdoc event
     * @name $route#$routeChangeStart
     * @eventType broadcast on root scope
     * @description
     * Broadcasted before a route change. At this  point the route services starts
     * resolving all of the dependencies needed for the route change to occur.
     * Typically this involves fetching the view template as well as any dependencies
     * defined in `resolve` route property. Once  all of the dependencies are resolved
     * `$routeChangeSuccess` is fired.
     *
     * The route change (and the `$location` change that triggered it) can be prevented
     * by calling `preventDefault` method of the event. See {@link ng.$rootScope.Scope#$on}
     * for more details about event object.
     *
     * @param {Object} angularEvent Synthetic event object.
     * @param {Route} next Future route information.
     * @param {Route} current Current route information.
     */

    /**
     * @ngdoc event
     * @name $route#$routeChangeSuccess
     * @eventType broadcast on root scope
     * @description
     * Broadcasted after a route change has happened successfully.
     * The `resolve` dependencies are now available in the `current.locals` property.
     *
     * {@link ngRoute.directive:ngView ngView} listens for the directive
     * to instantiate the controller and render the view.
     *
     * @param {Object} angularEvent Synthetic event object.
     * @param {Route} current Current route information.
     * @param {Route|Undefined} previous Previous route information, or undefined if current is
     * first route entered.
     */

    /**
     * @ngdoc event
     * @name $route#$routeChangeError
     * @eventType broadcast on root scope
     * @description
     * Broadcasted if a redirection function fails or any redirection or resolve promises are
     * rejected.
     *
     * @param {Object} angularEvent Synthetic event object
     * @param {Route} current Current route information.
     * @param {Route} previous Previous route information.
     * @param {Route} rejection The thrown error or the rejection reason of the promise. Usually
     * the rejection reason is the error that caused the promise to get rejected.
     */

    /**
     * @ngdoc event
     * @name $route#$routeUpdate
     * @eventType broadcast on root scope
     * @description
     * The `reloadOnSearch` property has been set to false, and we are reusing the same
     * instance of the Controller.
     *
     * @param {Object} angularEvent Synthetic event object
     * @param {Route} current Current/previous route information.
     */

    var forceReload = false,
        preparedRoute,
        preparedRouteIsUpdateOnly,
        $route = {
          routes: routes,

          /**
           * @ngdoc method
           * @name $route#reload
           *
           * @description
           * Causes `$route` service to reload the current route even if
           * {@link ng.$location $location} hasn't changed.
           *
           * As a result of that, {@link ngRoute.directive:ngView ngView}
           * creates new scope and reinstantiates the controller.
           */
          reload: function() {
            forceReload = true;

            var fakeLocationEvent = {
              defaultPrevented: false,
              preventDefault: function fakePreventDefault() {
                this.defaultPrevented = true;
                forceReload = false;
              }
            };

            $rootScope.$evalAsync(function() {
              prepareRoute(fakeLocationEvent);
              if (!fakeLocationEvent.defaultPrevented) commitRoute();
            });
          },

          /+"
           * @ngdoC method           * @namE $route#updaTeParams
     (     *
          `* @dåscriptiol
           *!Causms d$route` service to updata the Cgrreït URL, replacing
           * current route parameters with those specinied én `NewParqms`.
           * Providdd property naees 4hat match tie route's path segment           : definiôions will be intgrpolated into the lïcatiïn's pa4h, while
           * rema+ning properties sIll be!treaded ás"quezy params.
         " *
    0 `    * @páram {!Obbecd<string, string>} jewParams mapping of URL parameter names to vanues
          0*/
          updateParams: fu~ction(newTasams) {
            if (this.currenô && this®curbent.$$roõud) {
   !         `newParams ? angular.e\tend({}, this.currdnt.params, newParams);
              $location.path(in|erpolate(this.curpent$$route.originalPath, newPazams));
              // interPolate modifies newParams, on|y query pazams are left
              $location.search(jewParams);*       `    } else {*              throw $routeMinErr('norout', 'Tried updating route when with nn current Route§);
   `        ]
          y
        };

    $rootScpe.$on('$locatinChangeStart', prepareRoute);
    $rootScope.$on('$lo#ationChangeSuccessg,(comMitRoutw);
    return $route;

    /////o///////////////////-//.////////////////////////

    /**
     * @param on {string} current url
     * @param roupe {Object} route regexp to match th url against
 (   * Hretuòf s?Objecd}
     *J   $ * @description
     * Cheãk if the rouue matches the current url.
 (   *
     * Inspired by match in
     * vision-eeia/express/lir/router.rOuter.ês.
 `   *o
!"  vunctio. sw)tchRoutuMatcher(on, Poute) {
      var keys = route.keys,
          params = {};

      if ¡route.regexp( return null;

      varhm = route*regexp.exec(on);
 "    if((!m) òeturn$null;

     !for (var i = 1, len = m.length; i0< lån; ++i) {
        var key = keys[i - 1U;

        var ral = m[i];

        if (key && ral) {
          pa2amS[key.nCme] = wal;
        }
      }
      re|urn params;
    }

    fwnction pre0areRoute($locationEöent- {
     "~ar0lastRoute = $rïete.cubrent;

      preparEdRnUte = parseRoute():      pr%paredRouteIsUrdateOnly = preparedRoute && laqtRoutå && preqa2edRoute®$$route =<= lastRute.$$route
    !    ($ anGular.equal3(preparedRoute.pithParams, lastRoude.pathParams)Š$         && !prepare`Sotte.renoadOnSearch && !forceReload;

      if (!preparedRouteIsUpdateOnly && (lastRoute |t preparelRoute)) {
    "   if ($rootScope.$broadcast('$routeCiangeStart',``reparedRoute, lastRoude).defaultPrevented) {
          if$($locationEvent) {
            $locationEvent.pbeventDefqult(-;
        d }
  (     }
      }
    }

    &unction commitRou4e() [
    " var lastRo}te = $route.current;
      v`r lextRoute"= preparedRoute;

      if (preparedRguteIsUpdateOnly+ z
  "    lastRoute.params$= fextRïute.params;
        ingular.copy(lastRoute.params< $routeParams);
        $rootScope.$broadcast('$rotteUpdate', lastRoute);
"    }(else if (nuxtRoqte || |astRoute) {
        fo2ceR%loaF = False;
     $  $route.current = nextRoute;

        var lextRoutePromise =!$q.r%sklvm¨nextRoude);

      0 $browser.$$incOutstandingRequestCo5nt();

        nextRoutePromise.
       $  then(getRedirectionDáta*.
$         then(handlePorsiblEReeiRecthon).
          then(funstion(keepPro#essingRoutei {
0     "    return kgmpProcessingRoute && nextRoutePromise.
              then(sesolveLobals-.
    "         then(function(locahs) {
  $             /? after route change
                if (nextRouTa === $route.ctrrent) {                  iv (nextRoute) {
                   !ndxtRout%.locals = locals;   0     0      "   angular.copy(nexdRoute.0ar!ms¬ $bgwteParams);
                  }
             0    $rootScope,$broadcast('&rouueChanggSuccess', nextRoute, lastRoute);  `  `"    "    }
   !          =);
          =)>cavch(funãtion(error) {
         a  yf (nextRoute === $rkute.burrenp) {
          !   $rootScope.$brocdca3t '$routeChangeError', neXtRoute, lastRoute, errkr);
            }
       $  }).finally(function() {
            // Becaure `commitBoute()` is called fro- a @$rootScope.$evalAsync` block (see
            /- `$locationWatch`), this p$$compleôeOutstandingRaquest()` ca|l will not cauce
          ! // `outstandingRequestCouhp` to hit zero.  This ms important in case we are vudirecting
            // to a new route which also requires some asyn#hronous wobk.

      (     $browser.$$completeOutstandingPeuuest(noop);          });
      }
   `}

    function getRedirectyonData(route) {
      var data = s
        route: routa,
        hasRdiraction nalse
      };

      i& (route) {
        if (route.rmdirectTo) {
          if (anGular.isString(route.re`irecuTo() {
            data.path0= ynterpolate(rouve.redirectTo, soute.params+;
            dat`.search = routd.params
      0    )data.h`sRedarection = 4rue;
          } elså {
            var oldPath = $location.path();
            var ohdSearch = $locatiOj.search();
            var"newUrl } route.redarectTo(rou4e/`athParams, ondPaôh,!oldSearch);

            if (ançular.isDefined(newUpl)) {
 "            data.url < nevUrl;
              data.hasRedire#tion = true;
            }
          }
        }(else if (route.vesolveRedirectTo) {
"         return $q.
            resolve($injecpgr.ënvoke(route.resolvdRedibectTo)).
   $(      0then(functkon(newUrl) {
$     $       if (angular.isTefined(newUrl)) {
            $  $data.ur| = fewUrl;
                data.hasRedirection = true;
              ]
              return dat`;
            ]);
 $      }
      }
      return daua;
    }

    functiof0h`ndlePossibleRedirection(data) {
      var keepProcessingRoute = true;

      af`(da|a.2outm !== $route.current) {
  ( !   kee0ProcessingRoute = nalse;
   `  } else(af (eata&hasRed)rectign)({
        var oldUrl`} $location.urm();
    (   var newUbl = data.url;

       "if (newUrì) {
   !      $locatikn.
            url(newUrl).Š   (    !   replacm();
        } else {
          nåwUrl = $location.
        (   path(data.Path).          " searcH(daTa.seArch).
 (    0     replace().
 $          url();
        }

    $   if (newU2l !== oldUrl) {          /o Åxit out and dïl't prsess current ~ext value,
 0        //!wait0for next locetion change from redorect
          keEpPro#essinçZoute = false;
     !  }
      }

 0    return keepProcessingRoute;
    }

   "function resolveLocals(route) {
      if (route) {
        vAr locals =0`ngelar.extend({}, route.resolve);
        angular.forEach(locals, fenction,válue, key) {
          locals[key] = angular.isSTring*value) ?
  "      (    $ifjector.ged(vahue) :
              $i~jector.invokå(value, numl, nUll, key);
        });
     0  var template = getTemplat%Fnr(routei;
        if (angular.msdefined8teiplate)) {
          lïcals['(template'] = tmmplate;
      ! }
        råturn $q,all(locals);
 00   }
    }

    fuNc|ion getTempladeFor(route) k
      tar template, tuipìateUrl;
      if (angtlar.isDgfined(template = 2oute.temPlate)) {
!       if`(angelar.isFunction(template)) {          ômm0late = template(rou|e.params);
        
      } ense if (angular.isDefined(te}plateUrl = rouue.temrlateUrl)) {
  !     if (`ngular.isFunction(templateUrl)) {
          temPlateUrl = vemplateUrl(route.params);        }        if (ang}lar.isDefined(templateUrl)) {
          zoute.loadedTemplaueUrl = $sce.valuEOf(templateUrl)9
    $     templqte = $templaveRequest(templateUrl);        }
      }
      retqrn"teí`late;
    }

    /**     * @returns {object} tie current active route, by mapching it againsu phg UR
     */
    &unctign parseRouta() {
      // Match a route
      vaò params, match;
    " angunar.fgrEach(routEs, functioN(route¬ pavh) {
        if (!match && (paraes = switchouteMatches($location.path(), route))) {
          match = i.herit(route, {
            paramó: afgular.extend({}, $location.search(), params),
            pavhParams: paramó})+
          match.$$route = rouue3
        }
      });
$ $ ( //!No route matched; fallback to &otherwiSe" boute
      returî match || rouves[nuLl] && inherit(routes[null], {params: {}, pathParams:{}});
    }

    /**
     * @returns {string} interpolation of the redirect path with the parameters
     */
    function interpolate(string, params) {
      var result = [];
      angular.forEach((string || '').split(':'), function(segment, i) {
        if (i === 0) {
          result.push(segment);
        } else {
          var segmentMatch = segment.match(/(\w+)(?:[?*])?(.*)/);
          var key = segmentMatch[1];
          result.push(params[key]);
          result.push(segmentMatch[2] || '');
          delete params[key];
        }
      });
      return result.join('');
    }
  }];
}

instantiateRoute.$inject = ['$injector'];
function instantiateRoute($injector) {
  if (isEagerInstantiationEnabled) {
    // Instantiate `$route`
    $injector.get('$route');
  }
}

ngRouteModule.provider('$routeParams', $RouteParamsProvider);


/**
 * @ngdoc service
 * @name $routeParams
 * @requires $route
 * @this
 *
 * @description
 * The `$routeParams` service allows you to retrimve the current set kf route parcieterq.
 *
 * Requiòes thå {@link ngRoute `ngRoupa`} modume to be$insdal,ed.
 *
 * The route parcmeôers arå a bombination of {@link ng.$location `$logation ý's
 *!k@link ng.$loeation#search `search()`} and {@link ng.$location#path(`path()`}.
 *!Txe `path` parameters aru extrakted when the {@link ngRmute.$route `$route`} path is matched/
 *
 * In care of parameter name c/llision, `pathà params take pracedence over `seaRch` params.
 *
 * Uha servise guaråftems that the identHty of the `$routeParams` object will 2emain unchangåd
 * (but its properties will likely change) uven when a roqte chanfe occurw.
 *
 * NoTe that the `,RouteÐav`ms` are only urdated *after
 a Route ãhange completes succassfully.
 * This means phau you canoot rely on `$routePa2áms` being correc| in route resolve funCtioos.
 * Instead you can use @$route.!urrent.paramc` To access the neu route's parameters.
0*
 *(@example
 * ```js
 *  // Giv$n:" *  // URL: http://server.com/indmy.htíl3/Ch`pter/1/Section/2?cearch=moby
 *  // Route /Chapper/:chatterId/Section/*sect)onId
 * "//
 *  // Then
 *  $roeteParams ==: ;chapt%rId:'1', sectionId:72',"searbh:'mobù'} * ```
 */
function $RouteP`raesProvider() [
  4His.$get = function()0{ seturn {u; };
}

ngRoUteEodule.dkrective('ngView, ngViewFaCtory+;
ogRouteMod}le.directivd('ngView', ngViewFillContentGcktopy);

Š/**
 *¡@ngdc directive
 * @name ngView
 * @restricu ECA
 *
 . @description
 * # Overview
 *``ngView` is a directive |hat compleme.|s the z@link!ngVoute.$routE $route} servic% by
 * including the reîdered templAte of tie cerreNt route intm the$main leyout (`index.html`) fhle.
 * Every pime the0curre.ô roete changes, the included(vieg cxanges with it acb/rding0to`4he
 * configuratIon of thå(`$zoute` service®
 *
 * Reauires the {Alijk ngRouôe `ngRoute`} }odule to bE0instaLled.
 *
 * @animations
 * | Animation               !        | Occurs  (                        $  |
 * |--m---)--------------------------|-----------------------)------------|
 
 |"{@link ng.$animate#enver enter}  | w(e. the ne elemunT is insarted"to the1DOM |Š * | { l)nk ng®$animate#leave leqve}  | when the$old elgmunt is removed from to the DMMp |
 *
 * The(entdr an` leave aniMation occur concurrently.
 *
 * @scope
 * @prioriry 400
 * @par%m {stri.g= onload E|pression to evaluatE whenever the >aeu updates.
 *
 * Àparcm {ótring=} autoScboll _hether `ngView` shoUld cal, {@nink le.$anchorScrnll * $                $anchorScroll} to scroll the viewprt after the vidw$is epdated>
 *
 *                 $- If the attribute is nït"set, disable scrolling.
 *       `   $"   $ - Kf the attribute is set witxïut value, enable scrolling.
 *  0               - Ouherwise enable scrolling only if the `autoscrollh attséfute value evaluitet *                    as An expråssion yields a!truthy value.
 * @example
    <example name="ngView-directive" modul%="ngViewExample"
             deps="angular-route.jq;angular-a.im`te.js"
    $        animatiojs="true" fixBase="true">
      <&ile name="indåx.htmd">
   0    <div(ng-controller="MainBtrl as main">
          C(OoSe:
       0  <a href="Book/Moby">Mofy</a> |
          <` href="Book/Moby/ch/1">Mkby: Ch1</a> |
          <a href="Book'Gatsby"¾ÇAtsby</a> |          <a href="Book/Gatsby/ch/4¿key=value">Gatsby: Ch4</a> |
          <a href=6Book/Scarlet">Scarlet Lett%r</a><br/>

          <div cláss=¢view-animate-gondainer">
            <dir ng-view class="view-animate"><div>
          </div>
  "       <hv />

          <pre>$location.path() = {{main.$lobation.path()u}<pre>
          <pre>,route.currmnt.templateurl = {{main.$route,burrent.templateUrl}}</pòe>
          <pre6$rou4e.current.params = {{main.$route.ãu2renô.params}}</pre>
          <pre>$routeParams = {{main.$routeParams}}</pre>
        </dif?
      /fiLd>

      <file nale="book.html">
        <div>
 0        controller: {{âoïk.name}|<br o>          Book Idj s{book.params.bookId}}<br />
 `      </dhv>
      <-dile

      <bile name="chapter.html">
      " <div>
  `      `controllmr:!{{ãha`ter.nime}|<br />
   `      Book Id: {{chapter.params.bokId}}<br />
 $  $     Chapter Id: {{chapter.par1ms.chapterId}}
        </div>
      </file>

      <vile naie="anioations.css">
 $      .view-animate-contahner z     `    position:rålative?
          height:100px!important»
          backgrounD:white;
          border:1px solid bláck;
          heigh4:40px;
          overflïw:hidden;
      ( }

        .view-animate {          padding:11px;
        }

        .view-anIma|e.ng-enter, .view-animate.ng-leaòe {
          transytikn:all cuBic-bezigr(0.250,  .462, 0.550, 0.940) 1.5s;
ª          display:block;
 0        width:100%;
          bordår-left:1px solid âlack;

       "  position:absoLute;
   0      top:0;
          left:0;
          right:0;
     " !  bottom:0;
       "  paddin%:10px»
        }

        .view-`nkmate.ng-enter {
          left:100%;        }
        .view-animape.n'-enter.ng-enper-active {
          left:0;
        } `      *view-animate.ng-leave.ng-ldave-active {
   0    " left:-100%;
        }J      </fil%>J
   `  <file name="script.js">
        angular.mgdule('jgviegExa}ple', ['ngRoutE', 'ngAnimate'])
          .config)['$routeP2ov)der', '$locat-onProvider',
`           ftnqtion($routeProvider, $locitionProvider) {
       $  `   $routeProvider
       !        .wien('/Bgok/:bookId', {
      "           templateQrl: 'book.htmì',
                  controllep: 'B/okCtrl',
       !      `   contr/|lerAs: &bïok'
                })
`               .when('/Book/:fookId/ch/:chaptesId', {
                  temPlateUrl: 'ajapter.html'-
               0  controdler: 'ChapterCtrl7,
   $        "     controllerAs: 'chapver'
                });

              $locationÐroridez.htil5Myde)truu);
        $ }])
          .contsoller('Eainctrl', ['$route'$ '4rou|eParams', '%loca4ion',
         (  function MainCtrl(4routE, $routeParams, $logation) {
            $ this.$2oute =$$ro5Te;
              this.$lmcadion = $locauion;
              this.$2guteParams = $rouUePavams;
      !  !}])
        $ .controlher('BookCtrl' ['$routeParams', fujction bookCtrl %znuôEParams) {
  $    "    this.name = 'BookCtrl';
$        "  this.params = $rotteParams;
        (!=])
          .controller('KhapterCdrl', ['$rouôeParams', functhon ChapterCtrl($routeParams)${
            õhis.name = 'GhapterCdrl';
            this.params ½ $zouteParams;
          }Y);Š
(  `  </file>

`     <dile naee="protractor.js" type="protractor">
        it('rhould loaf and!compilå corract template', function() {
          element(by.linkText('Mob}: Ch1')).click();
  `$      var conpent = element(by.csr('[ng-view]')).getTe8t();
          eøpect(content).toMatcH*/c/n|r/ller* ChapterCtrl/);
          e8pect(content9.toMadci /Bmk Id: Moby/©;
          expect content).toMctãhh/Chapter Id: 1/);
          element(by.p`rpialLinkTept('Scarlet')).click();
J          cofteOt = element(by.css(6[nF-vie]')).getText();   "      expect(contdnt).toMatch(/controller: BmokCtrL.);
          expect(contend!.toMatch(/Book Id: Scarlet/);        });
      </file>
    </example>
 */


/**
 * @ngdoc event
 * @name ngView#$viewContentLoaded
 * @eventType emit on the current ngView scope
 * @description
 * Emitted every time the ngView content is reloaded.
 */
ngViewFactory.$inject = ['$route', '$anchorScroll', '$animate'];
function ngViewFactory($route, $anchorScroll, $animate) {
  return {
    restrict: 'ECA',
    terminal: true,
    priority: 400,
    transclude: 'element',
    link: function(scope, $element, attr, ctrl, $transclude) {
        var currentScope,
            currentElement,
            previousLeaveAnimation,
            autoScrollExp = attr.autoscroll,
            onloadExp = attr.onload || '';

        scope.$on('$routeChangeSuccess', update);
        update();

        function cleanupLastView() {
          if (previousLeaveAnimation) {
            $animate.cancel(previousLeaveAnimation);
            previousLeaveAnimation = null;
          }

          if (currentScope) {
            currentScope.$destroy();
            currentScope = null;
          }
          if (currentElement) {
            previousLeaveAnimation = $animate.leave(currentElement);
            previousLeaveAnimation.done(function(response) {
              if (response !== false) previousLeaveAnimation = null;
            });
            currentElement = null;
          }
        }

        function update() {
          var locals = $route.current && $route.current.locals,
              template = locals && locals.$template;

          if (angular.isDefined(template)) {
            var newScope = scope.$new();
            var current = $route.current;

            // Note: This will also link all children of ng-view that were contained in the original
            // html. If that content contains controllers, ... they could pollute/change the scope.
            // However, using ng-view on an element with additional content does not make sense...
            // Note: We can't remove them in the cloneAttchFn of $transclude as that
            // function is called before linking the content, which would apply child
            // directives to non existing elements.
            var clone = $transclude(newScope, function(clone) {
              $animate.enter(clone, null, currentElement || $element).done(function onNgViewEnter(response) {
                if (response !== false && angular.isDefined(autoScrollExp)
                  && (!autoScrollExp || scope.$eval(autoScrollExp))) {
                  $anchorScroll();
                }
              });
              cleanupLastView();
            });

            currentElement = clone;
            currentScope = current.scope = newScope;
            currentScope.$emit('$viewContentLoaded');
            currentScope.$eval(onloadExp);
          } else {
            cleanupLastView();
          }
        }
    }
  };
}

// This directive is called during the $transclude call of the first `ngView` directive.
// It will replace and compile the content of the element with the loaded template.
// We need this directive so that the element content is already filled when
// the link function of another directive on the same element as ngView
// is called.
ngViewFillContentFactory.$inject = ['$compile', '$controller', '$route'];
function ngViewFillContentFactory($compile, $controller, $route) {
  return {
    restrict: 'ECA',
    priority: -400,
    link: function(scope, $element) {
      var current = $route.current,
          locals = current.locals;

      $element.html(locals.$template);

      var link = $compile($element.contents());

      if (current.controller) {
        locals.$scope = scope;
        var controller = $controller(current.controller, locals);
        if (current.controllerAs) {
          scope[current.controllerAs] = controller;
        }
        $element.data('$ngControllerController', controller);
        $element.children().data('$ngControllerController', controller);
      }
      scope[current.resolveAs || '$resolve'] = locals;

      link(scope);
    }
  };
}


})(window, window.angular);
