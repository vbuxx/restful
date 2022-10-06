
function Insert() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.PetugasId = $("#petugasId").val();
    obj.Nama = $("#nama").val();
    obj.Supplier = $("#supplier").val();
    obj.Satuan = $("#satuan").val();
    obj.Qty = $("#qty").val();
    obj.Harga = $("#harga").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:5001/api/Barang/pengadaan",
        type: "POST",
        data: obj //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    }).done((result) => {
        //buat alert pemberitahuan jika success
        console.log(result)
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        console.log(error)
    })
}