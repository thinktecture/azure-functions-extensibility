module.exports = async function (context, req, products) {
    context.log('JavaScript ListProducts trigger function processed a request.');

    context.res.status(200).json(products
        .map(p => {
            return { id: p.ProductID, name: p.Name }
        }));
};