$(document).ready(() => {
    new IndexTask();
});

class IndexTask {
    private id: JQuery = $('#Id');
    private priority: JQuery = $('#Priority');
    private items: JQuery = $('#items');
    private description: JQuery = $('#Description');
    private btnAdd: JQuery = $('#btnAdd');

    private utilities;
    constructor() {
        this.utilities = new Utilities();

        this.fillTable();

        this.btnAdd.click(e => {
            if (this.id.val() != 0) {
                this.updateTask(false);
            } else {
                this.addTask();
            }
        });

        this.priority.append('<option class="dropdown-item" value="">Select</option>');
        this.utilities.manageRequest({
            url: 'Priority', type: 'GET', callback: response => {
                for (let item of response) {
                    this.priority.append('<option value="' + item.Id + '">' + item.Description + '</option>');
                }
            }
        })
        this.items.delegate('button', 'click', e => {
            if (e.target.getAttribute("Id").match('btnCompleted')) {
                var taskId = e.target.getAttribute("Id").replace("btnCompleted", "");
                this.completeTask(+taskId);
            } else if (e.target.getAttribute("Id").match('btnUpdate')) {
                var taskId = e.target.getAttribute("Id").replace("btnUpdate", "");
                this.actionBtnUpdate(+taskId);
            } else if (e.target.getAttribute("Id").match('btnDelete')) {
                var taskId = e.target.getAttribute("Id").replace("btnDelete", "");
                this.deleteTask(+taskId);
            }
        });


    }
    fillTable = () => {
        this.items.html('<tbody id="items"></tbody>');
        this.utilities.manageRequest({
            url: 'Task', type: 'GET', callback: response => {
                for (let item of response) {
                    this.items.append('<tr>'
                        + '<th scope="row"> <button type="button" class="btn btn-success" id="btnCompleted' + item.Id + '"' + (item.Completed ? 'disabled' : '') + '>Completed</button> </th>'
                        + '<td>' + item.Description + '</td>'
                        + '<td>' + item.Priority.Description + '</td>'
                        + '<td> <button type="button" class="btn btn-warning" id="btnUpdate' + item.Id + '"' + (item.Completed ? 'disabled' : '') + '>Update</button> </td>'
                        + '<td> <button type="button" class="btn btn-danger" id="btnDelete' + item.Id + '"' + (item.Completed ? 'disabled' : '') + '>Delete</button> </td>'
                        + '</tr>');
                }
            }
        })
    }

    actionBtnUpdate = (id: number) => {
        this.utilities.manageRequest({
            url: 'Task?id=' + id, type: 'GET', callback: response => {
                this.id.val(response.Id);
                this.description.val(response.Description);
                this.priority.val(response.PriorityId);
            }
        });
    };

    addTask = () => {
        this.utilities.manageRequest({
            url: 'Task', type: 'POST',
            data: {
                Id: 0,
                Description: this.description.val(),
                PriorityId: this.priority.val(),
                Completed: false
            },
            callback: response => {
                this.description.val('');
                this.priority.val(0);
                swal.fire('Added', 'Task added', 'success');
                this.fillTable();
            }
        })
    }

    updateTask = (completed: boolean) => {
        this.utilities.manageRequest({
            url: 'Task', type: 'PUT',
            data: {
                Id: this.id.val(),
                Description: this.description.val(),
                PriorityId: this.priority.val(),
                Completed: completed
            },
            callback: response => {
                this.description.val('');
                this.priority.val(0);
                swal.fire('Updated', 'Task updated', 'success');
                this.fillTable();
            }
        })
    }

    deleteTask = (id: number) => {
        this.utilities.manageRequest({
            url: 'Task?id=' + id, type: 'DELETE', callback: response => {
                this.fillTable();
            }
        });
    }

    completeTask = (id: number) => {
        this.utilities.manageRequest({
            url: 'Task?id=' + id, type: 'GET', callback: response => {
                this.id.val(response.Id);
                this.description.val(response.Description);
                this.priority.val(response.PriorityId);
                this.updateTask(true);
            }
        });
    }
}
