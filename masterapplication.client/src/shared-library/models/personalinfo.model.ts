import { BaseModel } from "./base.model";

export class PersonalInfo extends BaseModel {
  name: string;
  reportingPeriod?: string;
  unit: string;
  comd: string;
  typeOfReport: string;
  parentArm: string;
  apptHeld: string;
  dtOfCommission: string;
  medCategory: string;
  dtOfSeniority: string;
  category: string;
  awardOfWings: string;
  instrCategory: string;
  tosDate: Date;
  lastAppearedWithGebCeb: string;
  imgUrl: string;
  roleId: number;
  roleType: number;
}
