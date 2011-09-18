using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Services;

namespace KnowYourTurf.Web.Models.Vendor
{
    public class VendorViewModel : ViewModel
    {
        public IEnumerable<string> VendorContactNames{ get; set; }
        public Core.Domain.Vendor Vendor { get; set; }

        public string ChemicalInput { get; set; }
        public string FertilizerInput { get; set; }
        public string MaterialInput { get; set; }
        public IEnumerable<TokenInputDto> AvailableChemicals { get; set; }
        public IEnumerable<TokenInputDto> SelectedChemicals { get; set; }
        public IEnumerable<TokenInputDto> AvailableFertilizers { get; set; }
        public IEnumerable<TokenInputDto> SelectedFertilizers { get; set; }
        public IEnumerable<TokenInputDto> AvailableMaterials { get; set; }
        public IEnumerable<TokenInputDto> SelectedMaterials { get; set; }
    }
}