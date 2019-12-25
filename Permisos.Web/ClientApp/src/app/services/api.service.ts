import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError, pipe, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ApiService {
  public baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  get(url: string) {
    return this.request(url, 'GET');
  }

  post(url: string, body: Object) {
    return this.request(url, 'POST', body);
  }

  put(url: string, body: Object) {
    return this.request(url, 'PUT', body);
  }

  delete(url: string) {
    return this.request(url, 'DELETE');
  }

  request(
    url: string,
    method: 'GET' | 'POST' | 'PUT' | 'DELETE',
    body?: Object
  ) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
    };

    let httpRequest: Observable<any>;
    const requestUrl = `${this.baseUrl}${url}`;

    switch (method) {
      case 'POST':
        httpRequest = this.http.post(requestUrl, body, httpOptions);
        break;
      case 'PUT':
        httpRequest = this.http.put(requestUrl, body, httpOptions);
        break;
      case 'DELETE':
        httpRequest = this.http.delete(requestUrl, httpOptions);
        break;
      default:
        httpRequest = this.http.get(requestUrl, httpOptions);
    }

    return httpRequest.pipe(catchError((res: any) => this.onRequestError(res)));
  }

  onRequestError(res: any) {
    const statusCode = res.status;
    const body = res;

    const error = {
      statusCode: statusCode,
      error: body['error']
    };

    return throwError(error);
  }
}
