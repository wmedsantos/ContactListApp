import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class PersonService {

  constructor(private _http: HttpClient) {
   }

   addPerson(data: any): Observable<any> {
    return this._http.post('http://134.122.117.221:3000/api/person', data)
  }

}
