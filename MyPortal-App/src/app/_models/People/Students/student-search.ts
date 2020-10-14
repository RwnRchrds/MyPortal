import { PersonSearch } from './../person-search';
export class StudentSearch extends PersonSearch {
    status: string;
    curriculumGroupId: string;
    regGroupId: string;
    yearGroupId: string;
    houseId: string;
    senStatusId: string;

    constructor(){
        super();
        this.status = '1';
        this.curriculumGroupId = null;
        this.regGroupId = null;
        this.yearGroupId = null;
        this.houseId = null;
        this.senStatusId = null;
    }
}
