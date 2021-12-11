$(document).ready(function () {
    new IndexTask();
});
var IndexTask = /** @class */ (function () {
    function IndexTask() {
        var _this = this;
        this.id = $('#Id');
        this.priority = $('#Priority');
        this.items = $('#items');
        this.description = $('#Description');
        this.btnAdd = $('#btnAdd');
        this.fillTable = function () {
            _this.items.html('<tbody id="items"></tbody>');
            _this.utilities.manageRequest({
                url: 'Task', type: 'GET', callback: function (response) {
                    for (var _i = 0, response_1 = response; _i < response_1.length; _i++) {
                        var item = response_1[_i];
                        _this.items.append('<tr>'
                            + '<th scope="row"> <button type="button" class="btn btn-success" id="btnCompleted' + item.Id + '"' + (item.Completed ? 'disabled' : '') + '>Completed</button> </th>'
                            + '<td>' + item.Description + '</td>'
                            + '<td>' + item.Priority.Description + '</td>'
                            + '<td> <button type="button" class="btn btn-warning" id="btnUpdate' + item.Id + '"' + (item.Completed ? 'disabled' : '') + '>Update</button> </td>'
                            + '<td> <button type="button" class="btn btn-danger" id="btnDelete' + item.Id + '"' + (item.Completed ? 'disabled' : '') + '>Delete</button> </td>'
                            + '</tr>');
                    }
                }
            });
        };
        this.actionBtnUpdate = function (id) {
            _this.utilities.manageRequest({
                url: 'Task?id=' + id, type: 'GET', callback: function (response) {
                    _this.id.val(response.Id);
                    _this.description.val(response.Description);
                    _this.priority.val(response.PriorityId);
                }
            });
        };
        this.addTask = function () {
            _this.utilities.manageRequest({
                url: 'Task', type: 'POST',
                data: {
                    Id: 0,
                    Description: _this.description.val(),
                    PriorityId: _this.priority.val(),
                    Completed: false
                },
                callback: function (response) {
                    _this.description.val('');
                    _this.priority.val(0);
                    swal.fire('Added', 'Task added', 'success');
                    _this.fillTable();
                }
            });
        };
        this.updateTask = function (completed) {
            _this.utilities.manageRequest({
                url: 'Task', type: 'PUT',
                data: {
                    Id: _this.id.val(),
                    Description: _this.description.val(),
                    PriorityId: _this.priority.val(),
                    Completed: completed
                },
                callback: function (response) {
                    _this.description.val('');
                    _this.priority.val(0);
                    swal.fire('Updated', 'Task updated', 'success');
                    _this.fillTable();
                }
            });
        };
        this.deleteTask = function (id) {
            _this.utilities.manageRequest({
                url: 'Task?id=' + id, type: 'DELETE', callback: function (response) {
                    _this.fillTable();
                }
            });
        };
        this.completeTask = function (id) {
            _this.utilities.manageRequest({
                url: 'Task?id=' + id, type: 'GET', callback: function (response) {
                    _this.id.val(response.Id);
                    _this.description.val(response.Description);
                    _this.priority.val(response.PriorityId);
                    _this.updateTask(true);
                }
            });
        };
        this.utilities = new Utilities();
        this.fillTable();
        this.btnAdd.click(function (e) {
            if (_this.id.val() != 0) {
                _this.updateTask(false);
            }
            else {
                _this.addTask();
            }
        });
        this.priority.append('<option class="dropdown-item" value="">Select</option>');
        this.utilities.manageRequest({
            url: 'Priority', type: 'GET', callback: function (response) {
                for (var _i = 0, response_2 = response; _i < response_2.length; _i++) {
                    var item = response_2[_i];
                    _this.priority.append('<option value="' + item.Id + '">' + item.Description + '</option>');
                }
            }
        });
        this.items.delegate('button', 'click', function (e) {
            if (e.target.getAttribute("Id").match('btnCompleted')) {
                var taskId = e.target.getAttribute("Id").replace("btnCompleted", "");
                _this.completeTask(+taskId);
            }
            else if (e.target.getAttribute("Id").match('btnUpdate')) {
                var taskId = e.target.getAttribute("Id").replace("btnUpdate", "");
                _this.actionBtnUpdate(+taskId);
            }
            else if (e.target.getAttribute("Id").match('btnDelete')) {
                var taskId = e.target.getAttribute("Id").replace("btnDelete", "");
                _this.deleteTask(+taskId);
            }
        });
    }
    return IndexTask;
}());
//# sourceMappingURL=Index.js.map