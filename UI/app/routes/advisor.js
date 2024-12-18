import Route from '@ember/routing/route';
import { inject as service } from '@ember/service';

export default class AdvisorRoute extends Route {
  @service store;
  @service router;
  beforeModel() {
     
  }
  async model() {
    
    var data = await this.store.findAll('advisor');
    return data;
  }
}
