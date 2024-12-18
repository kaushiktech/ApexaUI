import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';

export default class AdvisorEditController extends Controller {
  @service store;
  @service session;
  @service router;
  gotoAdvisor() {
    this.router.transitionTo('advisor');
  }
  @action save(event) {
    event.preventDefault();
    var model = this.get('model');
    let gotoAdvisor=true;
    
    if (model.id > 0) {
        let gotoAdvisor=false;
        var advisor = this.store.findRecord('advisor', 1);
        advisor.sin = model.sin;
        advisor.address = model.address;
        advisor.phoneNumber = model.phoneNumber;
        advisor.fullName = model.fullName;
        try{
            advisor.save();
        }
        catch(e){
            console.log(e);
            gotoAdvisor=false;
        }
    }else{
        try{
            model.save();
        }
        catch(e){
            console.log(e);
            gotoAdvisor=false;
        }
    }
    
    if(gotoAdvisor){
        this.router.transitionTo('advisor');}
  }
}
