import EmberRouter from '@ember/routing/router';
import config from 'ui/config/environment';

export default class Router extends EmberRouter {
  location = config.locationType;
  rootURL = config.rootURL;
}

Router.map(function () {
  this.route('advisor', function () {
    this.route('edit');
    this.route('edit', { path: '/edit/:advisor_id' });
    this.route('view', { path: '/view/:advisor_id' });
  });
  this.route('login');
});
