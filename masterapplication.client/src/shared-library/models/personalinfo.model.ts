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
}

export class LastThreeAAAS extends BaseModel{
  personalInfoId: number;
  type: string;
  from: Date | string;
  to: Date | string;
  io: string;
  ro: string;
  sro: string;
}

export class AccidentalDetails extends BaseModel{
  personalInfoId: number;
  date: Date | string;
  acNoAndType: string;
  unitOrLOC: string;
  blamworthy: string;
  cause: string;
  statusPunish: string;
}

export class AdvExecRptRaised extends BaseModel{
  personalInfoId?: number;
  dt: string;
  unit: string;
  io: string;
  ro: string;
  sro: string;
  decisionByDG: string;
}

export class ForeignVisit extends BaseModel {
  personalInfoId: number;
  appt: string;
  fromDate: Date;
  toDate: Date;
  country: string;
  remark: string;
}

export class DvDtails extends BaseModel{
  personalInfoId: number;
  details: string;
}
