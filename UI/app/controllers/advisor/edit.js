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
    if (model.id > 0) {
        let gotoAdvisor=false;
      var self = this;
      this.store.findRecord('advisor', 1).then(function (advisor) {
        
        advisor.sin = model.sin;
        advisor.address = model.address;
        advisor.phoneNumber = model.phoneNumber;
        advisor.fullName = model.fullName;
        advisor
        .save()
        .then(() => {
            self.gotoAdvisor();
        })
        .catch((e) => {
            console.log(e);
        });
        
      });
      if(gotoAdvisor)
        this.gotoAdvisor();
    }else{
        var self = this;
        model
        .save()
        .then(() => {
            self.gotoAdvisor();
        })
        .catch((e) => {
            console.log(e);
        });
    }
  }
}
