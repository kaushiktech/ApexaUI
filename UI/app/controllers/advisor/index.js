import Controller from '@ember/controller';
import {action} from '@ember/object';
import { inject as service } from '@ember/service';
export default class AdvisorIndexController extends Controller {
    @service session;
    @service store;
    @action async deleteAdvisor(obj) {
        let advisor = this.store.peekRecord('advisor', obj.id);
        advisor.deleteRecord();
        advisor.isDeleted;
        await advisor
          .save()
          .then((idk) => {
            console.log('idk', idk);
          })
          .catch((error) => {
            console.error('omg' + error);
          });
      }
}
