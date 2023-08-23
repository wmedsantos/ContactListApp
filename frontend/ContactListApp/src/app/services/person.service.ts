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

  getPersonList(): Observable<any> {
    return this._http.get('http://134.122.117.221:3000/api/person')
  }

  deletePerson(id: string): Observable<any> {
    return this._http.delete(`http://134.122.117.221:3000/api/person/${id}`)
  }

  editPerson(data: any): Observable<any> {
    return this._http.put(`http://134.122.117.221:3000/api/person/${data.id}`,data);
  }
}

