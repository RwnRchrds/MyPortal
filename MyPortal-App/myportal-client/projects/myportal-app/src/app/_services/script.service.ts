import { Injectable } from '@angular/core';

declare var document: any;

@Injectable({
  providedIn: 'root'
})
export class ScriptService {

constructor() { }

loadScript(scriptUrl: string) {
  return new Promise(resolve => {
    const scriptElement = document.createElement('script');
    scriptElement.src = scriptUrl;
    scriptElement.onload = resolve;
    document.body.appendChild(scriptElement);
  });
}

loadStyleSheet(styleSheetUrl: string) {
  return new Promise((resolve, reject) => {
    const styleElement = document.createElement('link');
    styleElement.href = styleSheetUrl;
    styleElement.onload = resolve;
    styleElement.type = 'text/css';
    styleElement.rel = 'stylesheet';
    document.head.appendChild(styleElement);
  });
}

}
