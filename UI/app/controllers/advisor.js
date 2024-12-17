import Controller from '@ember/controller';
import { inject as service } from '@ember/service';
import { action } from '@ember/object';
export default class AdvisorController extends Controller {
  @service store;
  @service PopUp;
  @service session;
  @action openPopup() {
    this.PopUp.updateIt(true);
  }
  
}
