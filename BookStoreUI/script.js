const uri = 'https://localhost:7214/Api/authors';
let todos = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-first');
    const addlastTextbox=document.getElementById('add-last');
    const addbioTextbox=document.getElementById('add-bio');

    const item = {
        isComplete: false,
        firstname: addNameTextbox.value.trim(),
        lastname:addlastTextbox.value.trim(),
        bio:addbioTextbox.value.trim()

    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            addlastTextbox.value='';
            addbioTextbox.value='';
        })
        .catch(error => console.error('Unable to add item.', error));
}

 function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
} 

function displayEditForm(id) {
    const item = todos.find(item => item.id === id);
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-first').value = item.firstName;
    
    document.getElementById('edit-last').value = item.lastName;
    document.getElementById('edit-bio').value=item.bio;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        // isComplete: document.getElementById('edit-isComplete').checked,
      
        firstname: document.getElementById('edit-first').value.trim(),
        lastname:document.getElementById('edit-last').value.trim(),
        bio:document.getElementById('edit-bio').value.trim()

    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'to-do' : 'to-dos';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('todos');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
         let isCompleteCheckbox = document.createElement('input');
        isCompleteCheckbox.type = 'checkbox';
        isCompleteCheckbox.disabled = true;
        isCompleteCheckbox.checked = item.isComplete; 

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick',`displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isCompleteCheckbox);

        let td2 = tr.insertCell(1);
        let txtName = document.createTextNode(item.firstName);
        td2.appendChild(txtName);

        let td3=tr.insertCell(2);
        let txtlastname = document.createTextNode(item.lastName);
        td3.appendChild(txtlastname);

        let td4=tr.insertCell(3);
        let txtbio = document.createTextNode(item.bio);

        td4.appendChild(txtbio);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });

    todos = data;
}