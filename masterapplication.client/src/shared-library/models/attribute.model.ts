import { BaseModel } from "./base.model";

export class Category extends BaseModel{
  name:string;
}
export class NatureOfProject extends BaseModel{
  name:string;
}
export class ProjectStatus extends BaseModel{
  name:string;
}
export class DeleteModel {
  Id:number;
  TableName:string;
}
export class Feedback extends BaseModel{
  name:string;
  rank:string;
  unit:string;
  number:string;
  message:string;
}
