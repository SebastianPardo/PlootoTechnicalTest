$(document).ready(() => {
    new IndexTask();
});

class IndexTask {
    private utilities;
    constructor() {
        this.utilities = new Utilities();

        this.utilities.manageRequest({ url:'Task',type:'GET'})
    }
}
