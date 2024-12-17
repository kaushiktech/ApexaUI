import Route from '@ember/routing/route';
import { inject as service } from '@ember/service';
export default class AdvisorEditRoute extends Route {
  @service session;
  beforeModel(transition) {
    this.session.setup();
    this.session.requireAuthentication(transition, 'login');
  }
}
