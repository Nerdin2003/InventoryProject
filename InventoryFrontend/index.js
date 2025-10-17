const apiUrl = "http://localhost:5109/api/items"

async function fetchItems(){
    const response = await fetch(apiUrl);
    const items = await response.json();
    console.log(items);

    const itemList = document.getElementById("items-list");
    itemList.innerHTML = '';

    items.forEach(item => {
        const li = document.createElement('li');
        li.textContent = `${item.id} - Quantity: ${item.quantity}  - Price: $${item.price.toFixed(2)}`
        itemList.appendChild(li);
        
        const deleteButton = document.createElement('button');
        deleteButton.textContent = "Delete";
        deleteButton.onclick = () => deleteItem(item.id);
        li.appendChild(deleteButton);
    });

}

async function deleteItem(id) {
    //implement delete func
}

window.onload = fetchItems(); 