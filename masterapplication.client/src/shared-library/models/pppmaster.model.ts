import { BaseModel } from "./base.model";

export class PppMaster extends BaseModel {
  reference: string;
  sponsor: string;
  natureOfProject: string;
  projectDetails: string;
  estimatedCost: string;
  cashOutCost: string;
  category: string;
  type: string;
  priority: string;
  status: string;
  remarks: string;
}
