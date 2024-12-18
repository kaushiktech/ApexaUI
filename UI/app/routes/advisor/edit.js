import Route from '@ember/routing/route';
import { inject as service } from '@ember/service';
import { action } from '@ember/object';
export default class AdvisorEditRoute extends Route {
  @service session;
  @service store;
  async model(params) {
    console.log(params.advisor_id);
    if (params.advisor_id == 'new') return this.store.createRecord('advisor');
    else 
    return await this.store.findRecord('advisor', params.advisor_id);
  }
  beforeModel(transition) {
    this.session.requireAuthentication(transition, 'login');
  }
}
