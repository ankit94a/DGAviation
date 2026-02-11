import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../enviroments/environments.development';
import { ApiService } from './api.service';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class SamlHubService {
  public baseUrl = environment.baseUrl;
  private hubConnection! : signalR.HubConnection;
  public samlRequest$ = new BehaviorSubject<string>('');
  constructor(private router:Router ){
    this.startConnection();
  }
  startConnection(){
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl(this.baseUrl + 'saml-notification',{
      skipNegotiation:true,
      withCredentials:true,
      transport:signalR.HttpTransportType.WebSockets,
    }).withAutomaticReconnect().configureLogging(signalR.LogLevel.Information).build();

    this.hubConnection.start().then(()=>console.log('Saml Hub Connection started')).catch(err => console.error('Saml Hub Connection error',err))
    this.hubConnection.on('samlRequest',(samlRequest:any) =>{
      if(typeof samlRequest === "boolean"){
         this.router.navigate(['/log-out'])
      }else{
        
        window.location.href = `https://iam4.army.mil/IAM/logout?SAMLResponse=${samlRequest}` 
      }
     
   
    })
  }
  public onSamlResponse(callback:(data:any) => void):void{
    this.hubConnection.on('samlRequest',(data)=>{
      callback(data);
    })
  }
  stopConnection(){
    if(this.hubConnection){
      this.hubConnection.stop().then(()=>console.log("Saml Hub connction stopped")).catch(err => console.error('Error stopping saml Hub',err));
    }
  }

}
