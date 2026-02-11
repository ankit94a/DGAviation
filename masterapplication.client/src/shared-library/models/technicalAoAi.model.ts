import { BaseModel } from "./base.model";

export class TechnicalAoAi extends BaseModel{
  reference:string;
  subject:string;
  type:string;
}

export class ChatMessage {
  sender: 'user' | 'bot';
  text: string;
}