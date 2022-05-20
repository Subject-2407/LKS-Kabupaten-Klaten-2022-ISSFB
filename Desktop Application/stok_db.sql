CREATE DATABASE stok_db
GO
USE stok_db

CREATE TABLE users(
id_user INT NOT NULL IDENTITY,
nama VARCHAR(255),
password VARCHAR(255),
PRIMARY KEY(id_user)
)

CREATE TABLE pembeli(
id_pembeli INT NOT NULL IDENTITY,
nama VARCHAR(255),
alamat VARCHAR(255),
no_telp VARCHAR(255),
PRIMARY KEY(id_pembeli)
)

CREATE TABLE supplier(
id_supplier INT NOT NULL IDENTITY,
nama VARCHAR(255),
alamat VARCHAR(255),
no_telp VARCHAR(255),
PRIMARY KEY(id_supplier)
)

CREATE TABLE barang(
id_barang INT NOT NULL IDENTITY,
nama_barang VARCHAR(255),
harga INT,
id_supplier INT,
jns_barang VARCHAR(255),
jumlah INT,
keterangan VARCHAR(255),
PRIMARY KEY(id_barang),
FOREIGN KEY(id_supplier) REFERENCES supplier(id_supplier),
)

CREATE TABLE penjualan_barang(
id_penjualanbarang INT NOT NULL IDENTITY,
id_pembeli INT,
tanggal DATETIME,
jumlah INT,
id_barang INT,
status VARCHAR(255),
PRIMARY KEY(id_penjualanbarang),
FOREIGN KEY (id_pembeli) REFERENCES pembeli(id_pembeli),
FOREIGN KEY(id_barang) REFERENCES barang(id_barang),
)

CREATE TABLE pembelian_barang(
id_pembelianbarang INT NOT NULL IDENTITY,
id_barang INT,
id_supplier INT,
tanggal DATETIME,
jumlah INT,
harga INT,
keterangan VARCHAR(255),
PRIMARY KEY(id_pembelianbarang),
FOREIGN KEY(id_barang) REFERENCES barang(id_barang),
FOREIGN KEY(id_supplier) REFERENCES supplier(id_supplier),
)

INSERT INTO users VALUES
('Bintang','240705'),
('Admin','admin')