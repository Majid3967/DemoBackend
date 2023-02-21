using AutoMapper;
using Azure.Core;
using DemoBackend.Dtos;
using DemoBackend.Interfaces;
using DemoBackend.Models;
using DemoBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoBackend.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : Controller
    {
        private IItemInterface _itemRepository;
        private ICategoryInterface _categoryRepository;
        private IMapper _mapper;

        public ItemController(IItemInterface itemRepository,ICategoryInterface categoryInterface,IMapper mapper) {
            _itemRepository = itemRepository;
            _categoryRepository = categoryInterface;
            _mapper=mapper;
        }
        [HttpGet("getAllItems")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllItems()
        {
            var items = _mapper.Map<List<ItemDto>>(_itemRepository.GetAllItems());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(items);
        }

        [HttpGet("getItem/{itemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetItem(int itemId)
        {
            if (!_itemRepository.ItemExist(itemId))
                return BadRequest("Item Doesn't Exists");

            var item = _itemRepository.GetItemId(itemId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(item);
        }

        [HttpPost("addItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddItem([FromBody] ItemDto request)
        {
            if(request == null)
                return BadRequest(ModelState);

            if (_itemRepository.ItemExist(request.itemName))
                return BadRequest("Item Already Exists");
            if(!_categoryRepository.CategoryExist(request.categoryId))
                return BadRequest("Category Doesn't Exists");
            Item item = new Item();
            item.ItemId = _itemRepository.LastItem() + 1;
            item.ItemName = request.itemName.Trim();
            item.Description= request.description.Trim();
            item.ImageUrl= request.imageUrl.Trim();
            item.Price= request.price.Trim();
            item.CategoryId= request.categoryId;

            bool itemAdded = _itemRepository.AddItem(item);

            if (!itemAdded)
                return BadRequest("Something went wrong");

            return Ok("Item Successfully Added");
        }
    }
}
