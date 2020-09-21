import { ScriptService } from './../_services/script.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  schoolName: string = 'Harrods Community School';
  schoolMotto: string = 'Leap Into Learning';

  constructor(private scriptService: ScriptService) {}

  ngOnInit() {
    this.scriptService.loadStyleSheet('../../assets/lib/css/pages/login/login-2.css');
    this.scriptService.loadScript('../../assets/lib/js/pages/custom/login/login-general.js');
  }

}
