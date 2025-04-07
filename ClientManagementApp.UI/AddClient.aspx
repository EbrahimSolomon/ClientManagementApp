<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClient.aspx.cs" Inherits="ClientManagementApp.UI.AddClient" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multi Client Capture</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function addClientBlock() {
            var container = document.getElementById("clientContainer");
            var original = document.querySelector(".client-block");
            var clone = original.cloneNode(true);

            // Clear input values
            clone.querySelectorAll("input, select").forEach(el => el.value = "");

            // Add remove client button
            if (!clone.querySelector(".remove-client-btn")) {
                var btn = document.createElement("button");
                btn.type = "button";
                btn.className = "btn btn-danger btn-sm mt-2 remove-client-btn";
                btn.innerText = "❌ Remove This Client";
                btn.onclick = function () {
                    clone.remove();
                };
                clone.appendChild(btn);
            }

            // Ensure address/contact remove buttons only appear after cloning
            clone.querySelectorAll('.address-entry').forEach(addr => {
                addRemoveButtonToSection(addr, 'address');
            });
            clone.querySelectorAll('.contact-entry').forEach(cont => {
                addRemoveButtonToSection(cont, 'contact');
            });

            container.appendChild(clone);
        }

        function addRemoveButtonToSection(section, type) {
            if (!section.querySelector('.remove-' + type + '-btn')) {
                var btn = document.createElement("button");
                btn.type = "button";
                btn.className = "btn btn-danger btn-sm mt-2 remove-" + type + "-btn";
                btn.innerText = "❌ Remove " + (type === 'address' ? 'Address' : 'Contact');
                btn.onclick = function () {
                    section.remove();
                };
                section.appendChild(btn);
            }
        }

        function addAddressEntry(btn) {
            const addressBlock = btn.previousElementSibling.cloneNode(true);
            addressBlock.querySelectorAll("input, select").forEach(el => el.value = "");
            addRemoveButtonToSection(addressBlock, 'address');
            btn.parentNode.insertBefore(addressBlock, btn);
        }

        function addContactEntry(btn) {
            const contactBlock = btn.previousElementSibling.cloneNode(true);
            contactBlock.querySelectorAll("input, select").forEach(el => el.value = "");
            addRemoveButtonToSection(contactBlock, 'contact');
            btn.parentNode.insertBefore(contactBlock, btn);
        }

        function validateForm() {
            let isValid = true;

            document.querySelectorAll('[name="ContactType"]').forEach((typeEl, index) => {
                const valueEl = document.querySelectorAll('[name="ContactValue"]')[index];
                const type = typeEl.value;
                const value = valueEl.value.trim();

                if (type === "Mobile Number" && value !== "" && isNaN(value)) {
                    valueEl.classList.add("is-invalid");
                    isValid = false;
                } else {
                    valueEl.classList.remove("is-invalid");
                }
            });

            return isValid;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validateForm();">
        <div class="container mt-4">
            <asp:Label ID="lblStatus" runat="server" CssClass="text-success" Visible="false" /><br />

            <div id="clientContainer">
                <div class="client-block border p-3 mb-4">
                    <h5>Client</h5>
                    <div class="mb-3">
                        <label>First Name</label>
                        <input name="FirstName" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Last Name</label>
                        <input name="LastName" class="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Gender</label>
                        <select name="Gender" class="form-select">
                            <option value="">Select Gender</option>
                            <option>Male</option>
                            <option>Female</option>
                            <option>Other</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label>Date of Birth</label>
                        <input name="DateOfBirth" type="date" class="form-control" />
                    </div>

                    <h6>Addresses</h6>
                    <div class="addresses">
                        <div class="address-entry mb-3 border p-2">
                            <label>Address Type</label>
                            <select name="AddressType" class="form-select mb-1">
                                <option value="">Select Address Type</option>
                                <option value="Postal Address">Postal Address</option>
                                <option value="Physical Address">Physical Address</option>
                                <option value="Work Address">Work Address</option>
                                <option value="Home Address">Home Address</option>
                            </select>
                            <input name="Street" placeholder="Street" class="form-control mb-1" />
                            <input name="City" placeholder="City" class="form-control mb-1" />
                            <input name="Province" placeholder="Province" class="form-control mb-1" />
                            <input name="PostalCode" placeholder="Postal Code" class="form-control mb-1" />
                            <input name="Country" placeholder="Country" class="form-control mb-1" />
                        </div>
                        <button type="button" class="btn btn-secondary btn-sm mb-3" onclick="addAddressEntry(this)">➕ Add Address</button>
                    </div>

                    <h6>Contacts</h6>
                    <div class="contacts">
                        <div class="contact-entry mb-3 border p-2">
                            <select name="ContactType" class="form-select mb-1">
                                <option value="">Select Contact Type</option>
                                <option>Work Number</option>
                                <option>Email Address</option>
                                <option>Mobile Number</option>
                            </select>
                            <input name="ContactValue" placeholder="Contact Value" class="form-control mb-1" />
                            <div class="invalid-feedback">Mobile Number must be numeric.</div>
                        </div>
                        <button type="button" class="btn btn-secondary btn-sm mb-3" onclick="addContactEntry(this)">➕ Add Contact</button>
                    </div>
                </div>
            </div>

            <button type="button" onclick="addClientBlock()" class="btn btn-primary mb-3">➕ Add Another Client</button>

            <div class="mt-3">
                <asp:Button ID="btnSave" runat="server" Text="Save All" CssClass="btn btn-success" OnClick="btnSave_Click" />
                <asp:Button ID="btnExport" runat="server" Text="Export to CSV" CssClass="btn btn-secondary ms-2" OnClick="btnExport_Click" />
            </div>
        </div>
    </form>
</body>
</html>
