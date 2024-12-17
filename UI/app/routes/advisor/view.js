import Route from '@ember/routing/route';
import { inject as service } from '@ember/service';
import { action } from '@ember/object';
export default class AdvisorViewRoute extends Route {
  @service store;
  async model(params) {
    var data = await this.store.findRecord('advisor', params.advisor_id);
    return data;
  }
}
