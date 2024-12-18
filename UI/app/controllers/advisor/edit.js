import Controller from '@ember/controller';
import { action } from '@ember/object';
import { inject as service } from '@ember/service';
import  { tracked } from '@glimmer/tracking';

export default class AdvisorEditController extends Controller {
  @service store;
  @service session;
  @service router;
  @tracked errors='';
  @action
  async gotoAdvisor() {
    this.errors='';
    window.location.href='../../advisor';
    
  }
  get displayErrors() {
    
    return this.errors;
  }
  parseErrors(errors){
    
    //Hacky way of extracting errors from error response
    if(errors.errors[0]!=undefined)
    {
      var errors=JSON.parse(errors.errors[0].detail).errors;
      for(const key in errors){
          this.errors+='<li class="text-danger">'+errors[key][0]+'</li>';
      }
    }
    
  }
  @action save(event) {
    this.errors='';
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
          
            this.parseErrors(e);
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
          
          this.parseErrors(e);
        });
    }
    
  }
  
}
