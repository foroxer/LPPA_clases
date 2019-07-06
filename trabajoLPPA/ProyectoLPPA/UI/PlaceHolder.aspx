<link href="../Content/bootstrap.css" rel="stylesheet" />
<link href="../Content/placeholder.css" rel="stylesheet" />
<div class="col-sm-12">

  <main role="main" class="inner cover" style="box-shadow:none !important; background-color:none !important">
    <h1 class="cover-heading col-sm-12" style="text-align: center;color:black">Sitio en Construcción</h1>
    <h1 class="cover-heading col-sm-12" id="texto" style="text-align: center;color:black"></h1>
    <img src="../Content/underConstruction.png" class="col-sm-12"/>
   </main>
  <script type="text/javascript">
      window.onload = function () {
          document.getElementById("texto").innerText = window.parent.document.getElementById("caller").value;
      };
  </script>
</div>