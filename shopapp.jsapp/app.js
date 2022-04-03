
const getProducts = async () => {
    const response = await fetch("http://localhost:4000/api/products");
    const products = await response.json();

    // Cors

    setProducts(products); // [{"productId":"1", "name":"Iphone X"},{"productId":"2", "name":"Iphone 11"}]
}

const setProducts = (products) => {
    let container = document.querySelector(".tableBody");

    products.forEach(product => {
        container.innerHTML += `
        <tr>
            <td>${product.productId}</td>
            <td> <img src="../shopapp.webui/wwwroot/img/${product.imageUrl}" class="w-100"> </td>
            <td>${product.name}</td>
            <td>${product.url}</td>
            <td>${product.price}</td>
            <td>${product.rating}</td>
            <td>${product.stock}</td>
        </tr>
        `;
    });
}

getProducts();