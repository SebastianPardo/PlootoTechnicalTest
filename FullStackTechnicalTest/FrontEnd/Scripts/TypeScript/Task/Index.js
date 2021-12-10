$(document).ready(function () {
    new IndexTask();
});
var IndexTask = /** @class */ (function () {
    function IndexTask() {
        this.utilities = new Utilities();
        this.utilities.manageRequest({ url: 'Task', type: 'GET' });
    }
    return IndexTask;
}());
//# sourceMappingURL=Index.js.map