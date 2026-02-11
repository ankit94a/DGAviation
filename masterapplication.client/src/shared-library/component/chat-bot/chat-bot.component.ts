import { Component, Inject } from '@angular/core';
import { SharedLibraryModule } from '../../shared-library.module';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from '../../service/api.service';
import { ChatMessage } from '../../models/technicalAoAi.model';
import { FormControl } from '@angular/forms';
import { catchError, debounceTime, distinctUntilChanged, filter, map, Observable, of, switchMap } from 'rxjs';


@Component({
    selector: 'lib-chat-bot',
    imports: [SharedLibraryModule],
    templateUrl: './chat-bot.component.html',
    styleUrl: './chat-bot.component.css'
})

export class ChatBotComponent {
  isExpanded: boolean = false;
  userInput: string = '';
  messages: ChatMessage[] = [];
  exampleQuestions: string[] = [
    "What are EHL policies?",
    "How to apply for leave?",
    "Where can I find training schedules?",
    "Who do I contact for IT issues?",
    "What is the process for expense reimbursement?"
  ];

  myControl = new FormControl('');
  filteredOptions!:Observable<any[]>;
  constructor(@Inject(MAT_DIALOG_DATA) Data , private dialogRef: MatDialogRef<ChatBotComponent>,private apiService:ApiService){}
  ngOnInit(): void {
    this.messages.push({ sender: 'bot', text: "Hello, how can I assist you today?" });
    this.filteredOptions = this.myControl.valueChanges.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      filter(value => !!value && value.length >= 2),
      switchMap(value => this.getSuggestion(value))
    )
  }
  getSuggestion(value):Observable<any>{
    return this.apiService.getForChatBot(value,'dgeme').pipe(map((res:any) =>{
      const list = res?.data || [];
      return list;
    }))
  }

  selectOption(option){

  }
  trackByFn(index:number,item){
    return item;
  }
  displayFn(value){
    return value ? value : '';
  }
  toggleExpand() {
    this.isExpanded = !this.isExpanded;
  }
  sendMessage(){
  this.apiService.postForChatBot(this.myControl.value,"chatbot_dgeme",1).subscribe(res =>{
    if(res){
      this.messages.push({ sender: 'user', text: this.myControl.value});
      this.messages.push({ sender: 'bot', text: res.answer });
      this.myControl.setValue('');
    }
  })
  }
  // sendMessage(){
  //   if (!this.userInput.trim()) return;
  //   this.messages.push({ sender: 'user', text: this.userInput });
  //   this.apiService.postWithHeader('roleofmag/chatbot',this.userInput).subscribe(res =>{
  //     if(res){
  //       this.messages.push({ sender: 'bot', text: res });
  //       this.userInput = '';
  //     }
  //   })
  // }
  // selectExample(question: string) {
  //   this.userInput = question;
  //   this.sendMessage();
  // }
   close() {
    this.dialogRef.close(true);
  }

}
